using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour
{
    public TMP_Text m_text;
    //delay between each character
    public float m_delay = 0.5f;


    public void DisplayText(string _message)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(_message));
    }

    //reveal one character at a time
    private IEnumerator TypeText(string _message)
    {
        //set message and hide all characters
        m_text.text = _message;
        m_text.maxVisibleCharacters = 0;

        //reveal characters one by one until full message visible
        while (m_text.maxVisibleCharacters < _message.Length)
        {
            m_text.maxVisibleCharacters++;
            yield return new WaitForSeconds(m_delay);
        }
    }
}
