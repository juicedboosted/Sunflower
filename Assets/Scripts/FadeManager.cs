using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image m_fadeImage;
    public float m_fadeDuration= 3f;



    public void FadeToBlack()
    {
        StartCoroutine(FadeSetup());
    }

    public IEnumerator FadeSetup()
    {
        float timer = 0f;
        Color color = m_fadeImage.color;

        while (timer < m_fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer/m_fadeDuration);
            m_fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        m_fadeImage.color = color;

    }
}
