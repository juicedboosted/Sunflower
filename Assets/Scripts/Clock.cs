using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text m_clockText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_clockText.text = DateTime.Now.ToString("HH:mm");  
    }
}
