using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        //SceneManager.LoadScene(Options)
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
