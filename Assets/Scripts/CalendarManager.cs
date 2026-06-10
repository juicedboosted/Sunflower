using System.Globalization;
using TMPro;
using UnityEngine;

public class CalendarScript : MonoBehaviour
{
    public TMP_Text m_calendarText;

    public GameObject m_panel;
    public GameObject m_calendarAppPrefab;
    public Transform m_parentCanvas;

    public TMP_InputField m_earlyMorningInput;
    public TMP_InputField m_lateMorningInput;
    public TMP_InputField m_middayInput;
    public TMP_InputField m_afternoonInput;
    public TMP_InputField m_eveningInput;
    public TMP_InputField m_nightInput;

    private int m_currentMonth;
    private int m_currentYear;

    void Start()
    {
        //m_calendarText.text = DateTime.Now.ToString("dddd, dd MMM yyyy");
    }

    public void SpawnPanel()
    {
        Instantiate(m_calendarAppPrefab, m_parentCanvas);
    }
    
    public void ShowPanel()
    {
        m_panel.SetActive(true);
    }

    public void HidePanel()
    {
        m_panel.SetActive(false);
    }
}
