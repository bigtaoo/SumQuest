using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Settings
{
    private static Button Music;
    private static Button Sound;
    private static Image DisableMusic;
    private static Image DisableSound;
    private static readonly string MusicKey = "MusicEnabled";
    private static readonly string SoundKey = "SoundEnabled";

    public static void Initialize(Button music, Button sound, Image disable)
    {
        Music = music;
        Sound = sound;
        DisableMusic = disable;
        DisableSound = GameObject.Instantiate(DisableMusic, DisableMusic.transform.parent);
        DisableSound.transform.position = sound.transform.position;
        Music.onClick.AddListener(() => OnMusicClick());
        Sound.onClick.AddListener(() => OnSoundClick());
        DisableMusic.raycastTarget = false;
        DisableSound.raycastTarget = false;
    }

    public static void OnMusicClick()
    {
        var v = DisableMusic.IsActive() ? 0 : 1;
        PlayerPrefs.SetInt(MusicKey, v);
        SetDisableImage();
    }

    public static void OnSoundClick()
    {
        var v = DisableSound.IsActive() ? 0 : 1;
        PlayerPrefs.SetInt(SoundKey, v);
        SetDisableImage();
    }

    public static void SetDisableImage()
    {
        var musicEnabled = PlayerPrefs.GetInt(MusicKey, 0) == 1;
        DisableMusic.gameObject.SetActive(musicEnabled);
        var soundEnabled = PlayerPrefs.GetInt(SoundKey, 0) == 1;
        DisableSound.gameObject.SetActive(soundEnabled);

        if (musicEnabled)
        {
            SoundManager.Instance.SetMusicVolume(0.0f);
        }
        else
        {
            SoundManager.Instance.SetMusicVolume(0.2f);
        }

        if (soundEnabled)
        {
            SoundManager.Instance.SetSFXVolume(0.0f);
        }
        else
        {
            SoundManager.Instance.SetSFXVolume(1.0f);
        }
    }
}