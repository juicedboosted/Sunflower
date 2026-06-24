using UnityEngine;

public class AppManager : MonoBehaviour
{
    private GameObject activeApp = null;
    public GameObject m_HomeScreen;

    public void OpenApp(GameObject app)
    {
        activeApp = app;
        app.SetActive(true);
    }

    public void BackButton()
    {
        if (activeApp == null)
        {
            return;
        }
        MessagesManager messagesManagerComponent = activeApp.GetComponent<MessagesManager>();
        if (messagesManagerComponent != null) //if the open app is the messages app
        {
            if (messagesManagerComponent.m_conversationScreen.activeSelf) //and the user is looking at a conversation
            {
                messagesManagerComponent.CloseConversation();
                return;
            }
        }

        //otherwise, close the app
        CloseApp();
    }

    public void CloseApp()
    {
        if (activeApp != null)
        {
            activeApp.SetActive(false);
            activeApp = null; 
        }
    }
}
