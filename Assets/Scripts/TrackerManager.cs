using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackerManager : MonoBehaviour
{
    public Slider m_energySlider;
    public Slider m_healthSlider;

    public TMP_Text m_energyText;
    public TMP_Text m_calendarEnergyText;
    public TMP_Text m_healthText;

    public int m_maxEnergy = 60;
    public int m_minMaxEnergy = 20;

    private int m_scheduledTasks = 0;
    private bool m_ranOutOfEnergy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTracker();
    }

    public void UpdateTracker()
    {
        int energy = Mathf.RoundToInt(m_energySlider.value);
        int health = Mathf.RoundToInt(m_healthSlider.value);

        m_energyText.text = "Energy: " + energy + "/" + m_maxEnergy;
        m_healthText.text = "Health: " + health + "/" + m_maxEnergy;

        if (m_calendarEnergyText != null)
        {
            m_calendarEnergyText.text = energy + "/" + m_maxEnergy;
        }
    }

    public void AddScheduledTask()
    {
        m_scheduledTasks++;
        Debug.Log("Scheduled Tasks: " + m_scheduledTasks);
    }

    public bool SpendEnergy(int _amount)
    {
        int currentEnergy = Mathf.RoundToInt(m_energySlider.value);

        if (_amount <= 0)
        {
            return true;
        }
        if (currentEnergy < _amount)
        {
            Debug.Log("Not enough energy!!!!");
            return false;
        }

        m_energySlider.value -= _amount;

        if (m_energySlider.value <= 0)
        {
            m_ranOutOfEnergy = true;
        }

        UpdateTracker();
        return true;
    }

    public void StartNextDay()
    {
        if (m_ranOutOfEnergy)
        {
            m_maxEnergy--;
            if (m_maxEnergy < m_minMaxEnergy)
            {
                m_maxEnergy = m_minMaxEnergy;
            }
        }

        m_energySlider.maxValue = m_maxEnergy;
        m_energySlider.value = m_maxEnergy;
        m_scheduledTasks = 0;
        m_ranOutOfEnergy = false;
        UpdateTracker();
    }

}
