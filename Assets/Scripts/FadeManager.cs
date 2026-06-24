using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image m_fadeImage;
    public float m_fadeDuration= 3f;
    public float m_betweenDaysFadeDuration = 1f;


    public GameObject m_endPanel;
    public TypeWriterEffect m_typewriter;

    public string m_endMessage;

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

        m_endPanel.SetActive(true);
        m_typewriter.DisplayText(m_endMessage);
    }

    public void FadeToNextDay()
    {
        StartCoroutine(FadeUnfade());
    }

    public IEnumerator FadeUnfade()
    {
        float timer = 0f;
        Color color = m_fadeImage.color;

        //fade out
        while (timer < m_betweenDaysFadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / m_betweenDaysFadeDuration);
            m_fadeImage.color = color;

            yield return null;
        }
        timer = 0f;
        while (timer < m_betweenDaysFadeDuration / 2)
        {
            timer += Time.deltaTime;
            //do nothing else here so there's some space before fading back
        }
        timer = 0f;
        //fade back in
        while (timer < m_betweenDaysFadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / m_betweenDaysFadeDuration);
            m_fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        m_fadeImage.color = color;

    }
}
