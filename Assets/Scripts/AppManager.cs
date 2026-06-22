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

    public void CloseApp()
    {
        if (activeApp != null)
        {
            activeApp.SetActive(false);
            activeApp = null; 
        }
    }
}
