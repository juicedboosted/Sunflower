using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip m_backgroundMusic;
    public AudioClip m_creditMusic;
    private AudioSource m_audioSource;
    public GameObject m_clickSoundObject;

    private float m_soundEffectsVolume = 1f;

    private static MusicManager m_instance;

    public void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);
        m_audioSource = GetComponent<AudioSource>();

        LoadVolumeSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        HandleSceneMusic(SceneManager.GetActiveScene());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource clickSound = m_clickSoundObject.GetComponent<AudioSource>();
            if (clickSound != null)
            {
                clickSound.Play();
            }
        }
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        HandleSceneMusic(_scene);
    }

    private void HandleSceneMusic(Scene _scene)
    {
        if (_scene.name == "CreditScene")
        {
            PlayCreditMusic();
        }
        else if (_scene.name == "OptionsScene") {
            return;
        }
        else
        {
            PlayBackgroundMusic();
        }
    }

    private void PlayCreditMusic()
    {
        if (m_audioSource.clip == m_creditMusic)
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
        if (m_backgroundMusic == null)
        {
            return;
        }
        m_audioSource.Stop();
        m_audioSource.clip = m_backgroundMusic;
        m_audioSource.loop = true;
        m_audioSource.volume = 1f;
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
        PlayerPrefs.GetFloat("BackgroundMusicVolume", _volume);
    }

    public void SetSoundEffectsVolume(float _volume)
    {
        m_soundEffectsVolume = _volume;
        PlayerPrefs.GetFloat("SoundEffectsVolume", _volume);
    }

    public float GetSoundEffectsVolume()
    {
        return m_soundEffectsVolume;
    }

    public float GetBackgroundMusicVolume()
    {
        return m_audioSource.volume;
    }

}
