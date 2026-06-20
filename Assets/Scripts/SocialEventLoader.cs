using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Timeline;
using Unity.VisualScripting.Dependencies.Sqlite;

public class SocialEventLoader : MonoBehaviour
{
    [SerializeField] DialogueObject[] allDialogueObjects;

    private void Start() //TODO: this function only used for testing
    {
        SocialEvent coolEvent = LoadEvent("SocialEvents/TestEvent");
    }

    public enum MessageTime
    {
        MORNINGBEFORE,
        EVENINGBEFORE,
        MORNINGAFTER,
        EVENINGAFTER,
    }

    public struct EventMessage
    {
        public string characterName;
        public bool isSentByPlayer;
        public string message;
        public List<string> possibleResponses;
        public MessageTime timeOfDay;
    }

    public struct SocialEvent
    {
        public List<EventMessage> messages;
        public bool eventHasPassed;
    }

    public SocialEvent LoadEvent(string _filepath)
    {
        SocialEvent newEvent = new SocialEvent();
        newEvent.messages = new List<EventMessage>();
        newEvent.eventHasPassed = false;

        EventMessage currentMessage = new EventMessage();
        currentMessage.possibleResponses = new List<string>();
        bool enteringMessageStruct = false;
        bool enteringCharacterName = true;

        string currentResponse = "";
        bool enteringResponse = false;

        var eventFile = Resources.Load<TextAsset>(_filepath);
        for (int i = 0; i < eventFile.text.Length; i++)
        {
            if (eventFile.text[i] == '~')
            {
                if (enteringMessageStruct)
                {
                    //Add the message to the event
                    newEvent.messages.Add(currentMessage);
                    enteringMessageStruct = false;
                }
                else
                {
                    //prepare a new message to add
                    currentMessage = new EventMessage();
                    currentMessage.isSentByPlayer = false;
                    currentMessage.possibleResponses = new List<string>();
                    enteringMessageStruct = true;
                    enteringCharacterName = true;
                }
                continue;
            }

            //Break if no struct it being filled
            if (!enteringMessageStruct)
            {
                continue;
            }

            else if (eventFile.text[i] == '|') //finish entering the character's name
            {
                enteringCharacterName = false;
            }
            else if (eventFile.text[i] == '[') //begin entering a response
            {
                enteringResponse = true;
            }
            else if (eventFile.text[i] == ']') //finish entering a response
            {
                enteringResponse = false;
                //Add the response to the list of possible responses
                currentMessage.possibleResponses.Add(currentResponse);
                currentResponse = new string("");
            }
            else if (eventFile.text[i] == '{') //Take in the next character as time and skip ahead
            {
                currentMessage.timeOfDay = (MessageTime)(eventFile.text[i + 1] - '0');
                i += 2; //move the counter along to skip the message time declaration
            }
            else
            {
                if (enteringMessageStruct)
                {
                    if (enteringCharacterName)
                    {
                        currentMessage.characterName += eventFile.text[i];
                    }
                    else if (enteringResponse)
                    {
                        currentResponse += eventFile.text[i];
                    }
                    else
                    {
                        currentMessage.message += eventFile.text[i];
                    }
                }
            }
        }

        //Put messages in the lists held by the relevant dialogue objects
        for (int i = 0; i < newEvent.messages.Count; i++)
        {
            for (int j = 0; j < allDialogueObjects.Length; j++)
            {
                if (allDialogueObjects[j].characterName == newEvent.messages[i].characterName)
                {
                    /* all messages that are to arrive the morning before the event are to be put straight 
                    into the received messages list */
                    if (newEvent.messages[i].timeOfDay == MessageTime.MORNINGBEFORE)
                    {
                        allDialogueObjects[j].receivedMessages.Add(newEvent.messages[i]);
                    }
                    else
                    {
                        allDialogueObjects[j].queuedMessages.Add(newEvent.messages[i]);
                    }
                    break;
                }
            }
        }

        return newEvent;
    }
}