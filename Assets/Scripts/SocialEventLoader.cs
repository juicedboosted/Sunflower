using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Timeline;

public class SocialEventLoader : MonoBehaviour
{
    private void Start() //TODO: this function only used for testing
    {
        SocialEvent coolEvent = LoadEvent("SocialEvents/TestEvent");
        for (int i = 0; i < coolEvent.messages.Count; i++)
        {
            string debugMessage = coolEvent.messages[i].timeOfDay + "... " + coolEvent.messages[i].characterName + ": " + coolEvent.messages[i].message;
            Debug.Log(debugMessage);

            for (int j = 0; j < coolEvent.messages[i].possibleResponses.Count; j++)
            {
                debugMessage = "[" + coolEvent.messages[i].possibleResponses[j] + "]";
                Debug.Log(debugMessage);
            }
        }
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
                    newEvent.messages.Add(currentMessage);
                    enteringMessageStruct = false;
                }
                else
                {
                    currentMessage = new EventMessage();
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

            else if (eventFile.text[i] == '|')
            {
                enteringCharacterName = false;
            }
            else if (eventFile.text[i] == '[')
            {
                enteringResponse = true;
            }
            else if (eventFile.text[i] == ']')
            {
                enteringResponse = false;
                currentMessage.possibleResponses.Add(currentResponse);
                currentResponse = new string("");
            }
            else if (eventFile.text[i] == '{')
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

        return newEvent;
    }
}