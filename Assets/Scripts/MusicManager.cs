using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioClip m_backgroundMusic;
    public AudioClip m_creditMusic;
    private AudioSource m_audioSource;

    private float m_soundEffectsVolume = 1f;

    private static MusicManager m_instance;

    void Start()
    {
        
        PlayBackgroundMusic();
    }

    public void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name == "CreditScene")
        {
            PlayCreditMusic();
        }
        else
        {
            PlayBackgroundMusic();
        }
    }

    private void PlayCreditMusic()
    {
        if (m_audioSource == m_creditMusic)
        {
            return;
        }
        m_audioSource.Stop();
        m_audioSource.clip = m_creditMusic;
        m_audioSource.loop = true;
        m_audioSource.Play();
    }

    private void PlayBackgroundMusic()
    {
        if (m_audioSource == m_backgroundMusic)
        {
            return;
        }
        m_audioSource.clip = m_backgroundMusic;
        m_audioSource.loop = true;
        m_audioSource.Play();
    }

    public void LoadVolumeSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 1f);
        m_soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
        m_audioSource.volume = musicVolume;
    }

    public void SetMusicVolume(float _volume)
    {
        m_audioSource.volume = _volume;
        PlayerPrefs.GetFloat("BackgroundMusicVolume", 1f);
    }

    public void SetSoundEffectsVolume(float _volume)
    {
        m_audioSource.volume = _volume;
        PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
    }

    public float GetSoundEffectsVolume()
    {
        return m_soundEffectsVolume;
    }

}
