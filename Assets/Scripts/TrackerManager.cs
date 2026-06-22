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
    public int m_maxHealth = 60;
    public int m_minMaxEnergy = 10;

    private int m_scheduledTasks = 0;
    private bool m_ranOutOfEnergy = false;

    public Image m_energyBattery;
    public Sprite m_highEnergySprite;
    public Sprite m_mediumEnergySprite;
    public Sprite m_lowEnergySprite;
    public Sprite m_zeroEnergySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTracker();
    }

    public void UpdateTracker()
    {
        int energy = Mathf.RoundToInt(m_energySlider.value);
        int health = Mathf.RoundToInt(m_healthSlider.value);

        if (energy >= 40)
        {
            m_energyBattery.sprite = m_highEnergySprite;
        }
        else if (energy >= 20)
        {
            m_energyBattery.sprite = m_mediumEnergySprite;
        }
        else if (energy > 0)
        {
            m_energyBattery.sprite = m_lowEnergySprite;
        }
        else
        {
            m_energyBattery.sprite = m_zeroEnergySprite;
        }

        m_energyText.text = "Energy: " + energy + "/" + m_maxEnergy;
        m_healthText.text = "Health: " + health + "/" + m_maxHealth ;

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
            m_healthSlider.value -= 10;
        }

        UpdateTracker();
        return true;
    }

    public void StartNextDay()
    {
        if (m_ranOutOfEnergy)
        {
            m_maxEnergy -= 20;
            if (m_maxEnergy < m_minMaxEnergy)
            {
                m_maxEnergy = m_minMaxEnergy;
            }
        }

        if (!m_ranOutOfEnergy && m_maxEnergy < 60)
        {
            m_maxEnergy += 10;
        }

        m_energySlider.maxValue = m_maxEnergy;
        m_energySlider.value = m_maxEnergy;
        m_scheduledTasks = 0;
        m_ranOutOfEnergy = false;
        UpdateTracker();
    }

}
