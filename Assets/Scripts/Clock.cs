using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    //ingame time display
    public TMP_Text m_clockText;

    //start time for each day
    public int m_startHour = 7;
    public int m_startMinute = 0;

    //tracker for how much time has passed since day started
    private float m_elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetClock();
    }

    // Update is called once per frame
    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        UpdateClockDisplay();
    }

    public void ResetClock()
    {
        m_elapsedTime = 0f;
        UpdateClockDisplay();
    }

    //converts elapsed time into hours and minutes then updates the UI accordingly
    private void UpdateClockDisplay()
    {
        //m_elapsedTime / 60 -> if wanting real minutes
        int minutesTotal = (m_startHour * 60) + m_startMinute + Mathf.FloorToInt(m_elapsedTime);

        int hour = (minutesTotal / 60) % 24;
        int minute = minutesTotal % 60;

        m_clockText.text = string.Format("{0:00}:{1:00}", hour, minute);
    }
}
