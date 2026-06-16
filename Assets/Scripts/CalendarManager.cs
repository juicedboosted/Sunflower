using NUnit.Framework;
using System.Globalization;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

using UnityEditor;

public class CalendarManager : MonoBehaviour
{
    public GameObject m_taskPrefab;
    public Transform m_taskGroup;

    public TrackerManager m_trackerManager;
    public List<TaskData> m_possibleTasks = new List<TaskData>();
    public int m_numberOfTasks = 4;
    
    public GameObject m_panel;
    public GameObject m_calendarAppPrefab;
    public Transform m_parentCanvas;
    public TMP_Text m_dateText;

    private int m_dayNumber = 1;

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
        }
    }

    public void NextDay()
    {
        m_dayNumber++;
        m_trackerManager.StartNextDay();
        //ClearTimeSlots
        GenerateRandomTasks();
        UpdateDateText();
    }

    public void ClearTimeSlots()
    {
        TimeSlot[] slots = FindObjectsByType<TimeSlot>(FindObjectsSortMode.None);
        foreach (TimeSlot slot in slots)
        {
            foreach (Transform child in slot.transform)
            {
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
            m_dateText.text = "Day " + m_dayNumber;
        }
    }
}

