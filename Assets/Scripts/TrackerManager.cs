using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int m_minMaxEnergy = 0;

    private int m_scheduledTasks = 0;
    private bool m_ranOutOfEnergy = false;

    //battery icons for visual energy representation
    public Image m_energyBattery;
    public Sprite m_highEnergySprite;
    public Sprite m_mediumEnergySprite;
    public Sprite m_lowEnergySprite;
    public Sprite m_zeroEnergySprite;

    public TMP_Text m_energyWarningText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTracker();
    }

    public void UpdateTracker()
    {
        int energy = Mathf.RoundToInt(m_energySlider.value);
        int health = Mathf.RoundToInt(m_healthSlider.value);

        //changes battery icons based on current energy level
        if (energy > 40)
        {
            m_energyBattery.sprite = m_highEnergySprite;
        }
        else if (energy > 20)
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

    //records how many tasks are scheduled for the day
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
        //prevent scheduling task if not enough energy
        if (currentEnergy < _amount)
        {
            Debug.Log("Not enough energy!!!!");
            StopCoroutine(nameof(ShowWarning));
            StartCoroutine(ShowWarning());
            return false;
        }
        //remove energy from player
        m_energySlider.value -= _amount;

        //energy penalty when reaching 0 energy
        if (m_energySlider.value <= 0)
        {
            m_ranOutOfEnergy = true;
            m_healthSlider.value -= 10;
        }

        UpdateTracker();
        return true;
    }

    //reset daily values and apply energy recovery
    public void StartNextDay()
    {
        //reduce max energy if player reached 0 yesterday
        if (m_ranOutOfEnergy)
        {
            m_maxEnergy -= 20;
            if (m_maxEnergy < m_minMaxEnergy)
            {
                m_maxEnergy = m_minMaxEnergy;
            }
        }
        
        //recover some max energy when keeping energy levels above 0
        if (!m_ranOutOfEnergy && m_maxEnergy < 60)
        {
            m_maxEnergy += 10;
        }

        //reset daily tracking
        m_energySlider.maxValue = m_maxEnergy;
        m_energySlider.value = m_maxEnergy;
        m_scheduledTasks = 0;
        m_ranOutOfEnergy = false;
        UpdateTracker();
    }

    public void GoCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    //show temp warning when player doesnt have enough energy for a task
    public IEnumerator ShowWarning()
    {
        m_energyWarningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        m_energyWarningText.gameObject.SetActive(false);
    }

}
