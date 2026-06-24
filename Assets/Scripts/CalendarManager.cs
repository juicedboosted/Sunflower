using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class CalendarManager : MonoBehaviour
{
    //stores tasks to appear on the following day
    private List<TaskData> m_tomorrowTasks = new List<TaskData>();

    //task generation and calendar UI
    public GameObject m_taskPrefab;
    public Transform m_taskGroup;
    public Transform m_timeSlotParent;

    //managers player energy/health
    public TrackerManager m_trackerManager;

    //selection of random tasks to be generated
    public List<TaskData> m_possibleTasks = new List<TaskData>();
    public int m_numberOfTasks = 4;
    
    public GameObject m_panel;
    public GameObject m_calendarAppPrefab;
    public Transform m_parentCanvas;
    public TMP_Text m_dateText;

    public Clock m_clock;

    public FadeManager m_fadeManager;

    [SerializeField] SocialEventLoader m_SocialEventLoader;

    private int m_dayNumber = 0;
    private string[] m_daysOfWeek =
    {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday"
    };

    //initalises calendar display and generates tasks
    void Start()
    {
        m_dayNumber = 0;
        UpdateDateText();
        GenerateRandomTasks();

        AddTomorrowTask("See Martin", 30);
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

    //creates a set of tasks everyday
    public void GenerateRandomTasks()
    {
        foreach (Transform child in m_taskGroup)
        {
            //removes previous existing tasks
            Destroy(child.gameObject);
        }
        //temporary list to avoid dupes
        List<TaskData> tempTasks = new List<TaskData>(m_possibleTasks);

        for (int i = 0; i < m_numberOfTasks; i++)
        {
            if (tempTasks.Count == 0)
            {
                return;
            }
            //randomly select tasks 
            int randomIndex = Random.Range(0, tempTasks.Count);
            TaskData selectedTask = tempTasks[randomIndex];

            GameObject newTask = Instantiate(m_taskPrefab, m_taskGroup);

            //create a task card
            DraggableTask draggableTask = newTask.GetComponent<DraggableTask>();

            if (draggableTask != null)
            {
                draggableTask.SetTask(selectedTask.m_taskName, selectedTask.m_energyCost);
            }
            tempTasks.RemoveAt(randomIndex);

            //generate tasks that were queued
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

    //goes to next game day and refreshes calendar
    public void NextDay()
    {
        //trigger ending sequence when friday is complete
        if (m_dayNumber >= m_daysOfWeek.Length - 1)
        {
            m_fadeManager.FadeToBlack();
            return;
        }
        else
        {
            m_fadeManager.FadeToNextDay();
        }

        m_dayNumber++;
        Debug.Log("Day number " + m_dayNumber);

        //reset player state and make new schedule
        m_trackerManager.StartNextDay();
        ClearTimeSlots();
        GenerateRandomTasks();
        m_clock.ResetClock();
        UpdateDateText();

        //load social events depending on day
        switch (m_dayNumber)
        {
            case 1:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day1");
                    AddTomorrowTask("Beach party", 80);
                    break;
                }
            case 2:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day2");
                    break;
                }
            case 3:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day3");
                    AddTomorrowTask("Have Wiremu over", 20);
                    break;
                }
            case 4:
                {
                    m_SocialEventLoader.LoadEvent("SocialEvents/Day4");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //remove all scheduled tasks
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
            int dayIndex = Mathf.Clamp(m_dayNumber, 0, m_daysOfWeek.Length - 1);
            m_dateText.text = m_daysOfWeek[dayIndex];
        }
    }

    //add task to queue to appear on the next day
    public void AddTomorrowTask(string _taskName, int _energyCost)
    {
        TaskData tomorrowTask = new TaskData();
        tomorrowTask.m_taskName = _taskName;
        tomorrowTask.m_energyCost = _energyCost;

        m_tomorrowTasks.Add(tomorrowTask);
        Debug.Log("Task added for tomorrow!!! -> " + _taskName);
    }
}

