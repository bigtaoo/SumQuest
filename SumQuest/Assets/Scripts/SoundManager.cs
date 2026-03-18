using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource musicSource;   // for background music
    private AudioSource sfxSource;     // for sound effects

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip numberMatchSound;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = 0.2f;

            sfxSource.playOnAwake = false;

            if (backgroundMusic != null)
            {
                PlayMusic(backgroundMusic);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
            // Debug.LogWarning("Play background music");
        }
    }

    // public void StopMusic()
    // {
    //     musicSource.Stop();
    // }

    public void SetMusicVolume(float volume)
    {
        // Debug.LogWarning($"Music volume: {volume}");
        musicSource.volume = volume;
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float volume)
    {
        // Debug.LogWarning($"SFX volume: {volume}");
        sfxSource.volume = volume;
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }

    public void PlayNumberMatch()
    {
        PlaySFX(numberMatchSound);
    }
}
