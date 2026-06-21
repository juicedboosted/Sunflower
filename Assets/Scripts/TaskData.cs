using UnityEngine;

public enum TaskType
{
    Study,
    Social,
    Chore,
    Appointment,
    Leisure
}


[System.Serializable]
public class TaskData
{
    public string m_taskName;
    public TaskType m_taskType;
    public int m_energyCost;
}
