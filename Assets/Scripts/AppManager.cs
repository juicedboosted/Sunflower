using UnityEngine;

public class AppManager : MonoBehaviour
{
    private GameObject activeApp = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
