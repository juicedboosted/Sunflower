using NUnit.Framework;
using System.Globalization;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

public class CalendarManager : MonoBehaviour
{
    //TOMORROW TASK LIST
    private List<TaskData> m_tomorrowTasks = new List<TaskData>();


    public GameObject m_taskPrefab;
    public Transform m_taskGroup;
    public Transform m_timeSlotParent;

    public TrackerManager m_trackerManager;
    public List<TaskData> m_possibleTasks = new List<TaskData>();
    public int m_numberOfTasks = 4;
    
    public GameObject m_panel;
    public GameObject m_calendarAppPrefab;
    public Transform m_parentCanvas;
    public TMP_Text m_dateText;

    public Clock m_clock;

    public FadeManager m_fadeManager;

    [SerializeField] SocialEventLoader m_SocialEventLoader;

    private int m_dayNumber = 1;
    private string[] m_daysOfWeek =
    {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday"
    };

    void Start()
    {
        UpdateDateText();
        GenerateRandomTasks();
       
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

    public void GenerateRandomTasks()
    {
        foreach (Transform child in m_taskGroup)
        {
            Destroy(child.gameObject);
        }
        List<TaskData> tempTasks = new List<TaskData>(m_possibleTasks);

        for (int i = 0; i < m_numberOfTasks; i++)
        {
            if (tempTasks.Count == 0)
            {
                return;
            }
            int randomIndex = Random.Range(0, tempTasks.Count);
            TaskData selectedTask = tempTasks[randomIndex];

            GameObject newTask = Instantiate(m_taskPrefab, m_taskGroup);
            DraggableTask draggableTask = newTask.GetComponent<DraggableTask>();

            if (draggableTask != null)
            {
                draggableTask.SetTask(selectedTask.m_taskName, selectedTask.m_energyCost);
            }
            tempTasks.RemoveAt(randomIndex);

            // TOMORROW TASK QUEUE
            foreach (TaskData queuedTask in m_tomorrowTasks)
            {
                GameObject tomorrowTask = Instantiate(m_taskPrefab, m_taskGroup);
                DraggableTask tomorrowDraggableTask = tomorrowTask.GetComponent<DraggableTask>();
                if (tomorrowDraggableTask != null)
                {
                    tomorrowDraggableTask.SetTask(queuedTask.m_taskName, queuedTask.m_energyCost);
                }
            }
            m_tomorrowTasks.Clear();
        }
    }

    public void NextDay()
    {
        if (m_dayNumber >= 5)
        {
            m_fadeManager.FadeToBlack();
        }

        m_dayNumber++;
        m_trackerManager.StartNextDay();
        ClearTimeSlots();
        GenerateRandomTasks();
        m_clock.ResetClock();
        UpdateDateText();

        switch (m_dayNumber)
        {
            case 0:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day0.txt");
                    break;
                }
            case 1:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day1.txt");
                    break;
                }
            case 2:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day2.txt");
                    break;
                }
            case 3:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day3.txt");
                    break;
                }
            case 4:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day4.txt");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void ClearTimeSlots()
    {
        if (m_timeSlotParent == null)
        {
            return;
        }

        foreach (Transform slot in m_timeSlotParent)
        {
            for (int i = slot.childCount - 1; i >= 0; i--)
            {
                Transform child = slot.GetChild(i);
                if (child.GetComponent<DraggableTask>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void UpdateDateText()
    {
        if (m_dateText != null)
        {
            int dayIndex = Mathf.Clamp(m_dayNumber - 1, 0, m_daysOfWeek.Length - 1);
            m_dateText.text = m_daysOfWeek[dayIndex];
        }
    }

    // TOMORROW TASK ADD FUNCTION
    public void AddTomorrowTask(string _taskName, int _energyCost)
    {
        TaskData tomorrowTask = new TaskData();
        tomorrowTask.m_taskName = _taskName;
        tomorrowTask.m_energyCost = _energyCost;

        m_tomorrowTasks.Add(tomorrowTask);
        Debug.Log("Task added for tomorrow!!! -> " + _taskName);
    }
}

