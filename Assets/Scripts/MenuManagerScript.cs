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
        SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
