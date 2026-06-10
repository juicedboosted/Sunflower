using System.Globalization;
using TMPro;
using UnityEngine;

public class CalendarScript : MonoBehaviour
{
    public TMP_Text m_calendarText;

    public GameObject m_panel;
    public GameObject m_calendarAppPrefab;

    private int m_currentMonth;
    private int m_currentYear;

    void Start()
    {

    }

    public void SpawnPanel()
    {
        Instantiate(m_calendarAppPrefab);
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
