using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour
{
    public TMP_Text m_text;
    public float m_delay = 0.5f;


    public void DisplayText(string _message)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(_message));
    }

    private IEnumerator TypeText(string _message)
    {
        m_text.text = _message;
        m_text.maxVisibleCharacters = 0;

        while (m_text.maxVisibleCharacters < _message.Length)
        {
            m_text.maxVisibleCharacters++;
            yield return new WaitForSeconds(m_delay);
        }
    }
}
