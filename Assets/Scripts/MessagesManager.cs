using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    public float m_messageOffset = 200.0f;
    public float m_messageSpacing = 100.0f;

    private DialogueObject m_currentConversation = null;
    private List<GameObject> m_currentMessages;

    [SerializeField] GameObject m_conversationScreen;
    [SerializeField] GameObject m_conversationListScreen;
    [SerializeField] GameObject m_conversationContent;
    [SerializeField] GameObject m_messagePrefab;

    public void OpenConversation(DialogueObject _conversation)
    {
        m_currentConversation = _conversation;
        m_conversationListScreen.SetActive(false);
        m_conversationScreen.SetActive(true);
        //TODO: instantiate a text box for each message struct held by the dialogue object 
        for (int i = 0; i < m_currentConversation.receivedMessages.Count; i++)
        {
<<<<<<< Updated upstream
            GameObject newMessage = Instantiate(m_messagePrefab, m_conversationScreen.transform.Find("Viewport").Find("Content"));
            newMessage.transform.position.Set(0, (i + 1.0f) * -m_messageSpacing, 0); //set the message position going down the screen
=======
            GameObject newMessage = Instantiate(m_messagePrefab, m_conversationContent.transform);
        }

        TextMessageInstance[] allTexts = m_conversationContent.GetComponentsInChildren<TextMessageInstance>();

        for (int i = 0; i < allTexts.Length; i++)
        {
            //Set the message instance's text
            allTexts[i].GetComponentInChildren<TextMeshProUGUI>().text = m_currentConversation.receivedMessages[i].message;
            //move the message down the screen by its size plus standard message spacing
            allTexts[i].transform.localPosition = new Vector3(0, 
                -m_messageOffset + (i * -(m_messageSpacing + allTexts[i].GetComponentInChildren<RectTransform>().sizeDelta.y)), 0);
>>>>>>> Stashed changes
        }
    }
}
