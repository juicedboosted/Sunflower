using UnityEngine;

public class AppManager : MonoBehaviour
{
    private GameObject activeApp = null;
    public GameObject m_HomeScreen;

    public void OpenApp(GameObject app)
    {
        activeApp = app;
        app.SetActive(true);
        m_HomeScreen.SetActive(false);
    }

    public void CloseApp()
    {
        if (activeApp != null)
        {
            activeApp.SetActive(false);
            activeApp = null; 
        }

        m_HomeScreen.SetActive(true);
    }
}
