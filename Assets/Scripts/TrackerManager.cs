using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackerManager : MonoBehaviour
{
    public Slider m_energySlider;
    public Slider m_moodSlider;

    public TMP_Text m_energyText;
    public TMP_Text m_moodText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTracker();
    }

    public void UpdateTracker()
    {
        int energy = Mathf.RoundToInt(m_energySlider.value);
        int mood = Mathf.RoundToInt(m_moodSlider.value);

        m_energyText.text = "Energy: " + energy + "/10";
        m_moodText.text = "Mood/Health: " + mood + "/10";
    }
}
