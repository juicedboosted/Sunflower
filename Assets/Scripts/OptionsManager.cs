using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public Slider m_backgroundMusicSlider;
    public Slider m_soundEffectsSlider;

    private MusicManager m_musicManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finds the music manager object across scene change
        m_musicManager = FindFirstObjectByType<MusicManager>();

        if (m_musicManager == null)
        {
            return;
        }
 
        m_backgroundMusicSlider.value = m_musicManager.GetBackgroundMusicVolume();
        m_soundEffectsSlider.value = m_musicManager.GetSoundEffectsVolume();
        m_backgroundMusicSlider.onValueChanged.AddListener(m_musicManager.SetMusicVolume);
        m_soundEffectsSlider.onValueChanged.AddListener(m_musicManager.SetSoundEffectsVolume);
    }

}
