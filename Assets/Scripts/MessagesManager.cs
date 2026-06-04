using System.Collections.Generic;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    public float m_messageSpacing = 100.0f;

    private DialogueObject m_currentConversation = null;
    private List<GameObject> m_currentMessages;

    [SerializeField] GameObject m_conversationScreen;
    [SerializeField] GameObject m_conversationListScreen;
    [SerializeField] GameObject m_messagePrefab;

    public void OpenConversation(DialogueObject _conversation)
    {
        m_currentConversation = _conversation;
        m_conversationListScreen.SetActive(false);
        m_conversationScreen.SetActive(true);
        //TODO: instantiate a text box for each message struct held by the dialogue object 
        for (int i = 0; i < m_currentConversation.m_messageCount; i++)
        {
            GameObject newMessage = Instantiate(m_messagePrefab, m_conversationScreen.transform.Find("Viewport").Find("Content"));
            newMessage.transform.position.Set(0, (i + 1.0f) * -m_messageSpacing, 0); //set the message position going down the screen
        }
    }
}
