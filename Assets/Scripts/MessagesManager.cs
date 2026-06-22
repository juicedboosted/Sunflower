using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 TODO:
- Make the difference between incoming and outgoing messages obvious
 */

public class MessagesManager : MonoBehaviour
{
    public float m_playerMessageOffsetX = 250.0f;

    public float m_messageOffset = 200.0f;
    public float m_messageSpacing = 100.0f;

    private DialogueObject m_currentConversation = null;

    [SerializeField] GameObject m_conversationScreen;
    [SerializeField] GameObject m_conversationListScreen;
    [SerializeField] GameObject m_conversationContent;
    [SerializeField] GameObject m_messagePrefab;

    [SerializeField] GameObject[] m_responseButtons;

    public void OpenConversation(DialogueObject _conversation)
    {
        m_currentConversation = _conversation;
        m_conversationListScreen.SetActive(false);
        m_conversationScreen.SetActive(true);
        //TODO: instantiate a text box for each message struct held by the dialogue object 
        for (int i = 0; i < m_currentConversation.receivedMessages.Count; i++)
        {
            GameObject newMessage = Instantiate(m_messagePrefab, m_conversationScreen.transform.Find("Viewport").Find("Content"));
        }

        //Load in all messages from this conversation
        TextMessageInstance[] allTexts = m_conversationContent.GetComponentsInChildren<TextMessageInstance>();

        //Return here if there are no messages as the following code will break in that circumstance
        if (allTexts.Length < 1)
        {
            return;
        }

        for (int i = 0; i < allTexts.Length; i++)
        {
            //Set the message instance's text
            allTexts[i].GetComponentInChildren<TextMeshProUGUI>().text = m_currentConversation.receivedMessages[i].message;
            //move the message down the screen by its size plus standard message spacing, and along depending on if it is sent by the player or not
            allTexts[i].transform.localPosition = new Vector3(m_currentConversation.receivedMessages[i].isSentByPlayer ? m_playerMessageOffsetX : 0, 
                -m_messageOffset + (i * -(m_messageSpacing + allTexts[i].GetComponentInChildren<RectTransform>().sizeDelta.y)), 0);
        }

        //Load in responses to the last message
        List<string> allResponses = m_currentConversation.receivedMessages[m_currentConversation.receivedMessages.Count - 1].possibleResponses;
        for (int i = 0; i < allResponses.Count; i++) //for each possible response, make a response button visible
        {
            if (i >= m_responseButtons.Length) //break if there are more responses than response buttons
            {
                break;
            }

            m_responseButtons[i].SetActive(true);
            m_responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allResponses[i];
        }
    }

    public void RespondToMessage(int _responseIndex)
    {
        //Create a new message with the response text from the player and add it to this conversation's received messages
        SocialEventLoader.EventMessage responseAsMessage = new SocialEventLoader.EventMessage();
        responseAsMessage.characterName = m_currentConversation.characterName;
        responseAsMessage.message = m_responseButtons[_responseIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        responseAsMessage.isSentByPlayer = true;
        responseAsMessage.possibleResponses = new List<string>();

        m_currentConversation.receivedMessages.Add(responseAsMessage);
        Debug.Log("Responded with index " + _responseIndex);

        //Instantiate the new message in the content window
        GameObject newMessageInstance = Instantiate(m_messagePrefab, m_conversationScreen.transform.Find("Viewport").Find("Content"));
        int messageCount = m_conversationContent.transform.childCount + 1;
        newMessageInstance.GetComponentInChildren<TextMeshProUGUI>().text = responseAsMessage.message;
        newMessageInstance.transform.localPosition = new Vector3(m_playerMessageOffsetX,
                (messageCount * -(m_messageSpacing + newMessageInstance.GetComponentInChildren<RectTransform>().sizeDelta.y)), 0);

        //Hide response buttons
        for (int i = 0; i < m_responseButtons.Length; i++)
        {
            m_responseButtons[i].SetActive(false);
        }
    }

    public void CloseConversation()
    {
        //hide the response buttons
        for (int i = 0; i < m_responseButtons.Length; i++)
        {
            m_responseButtons[i].SetActive(false);
        }

        //unload all text message instances
        TextMessageInstance[] allMessages = m_conversationContent.GetComponentsInChildren<TextMessageInstance>();

        for (int i = 0; i < allMessages.Length; i++)
        {
            Destroy(allMessages[i].gameObject);
        }

        //unassign the current conversation
        m_currentConversation = null;

        //Switch which screen is visible
        m_conversationListScreen.SetActive(true);
        m_conversationScreen.SetActive(false);
    }

    public void AdvanceTime(int _newTime) //Takes in an int to make unity buttons work
    {
        //get all conversations
        DialogueObject[] allConversations = m_conversationListScreen.GetComponentsInChildren<DialogueObject>();
        //for each conversation, iterate through its queued messages
        for (int i = 0; i < allConversations.Length; i++)
        {
            for (int j = 0; j < allConversations[i].queuedMessages.Count; j++)
            {
                //if any of them are meant to send at the new time, move them to the received messages section
                if (allConversations[i].queuedMessages[j].timeOfDay == (SocialEventLoader.MessageTime)_newTime)
                {
                    allConversations[i].receivedMessages.Add(allConversations[i].queuedMessages[j]);
                    allConversations[i].queuedMessages.RemoveAt(j);
                }
            }

        }
    }
}