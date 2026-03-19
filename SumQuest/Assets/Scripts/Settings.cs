using System.Collections.Generic;
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
    private static readonly string ButtonSpriteKey = "ButtonSprite";
    private static Dictionary<ButtonImageTypes, Button> Buttons = new();
    private static Image Select;
    public static Sprite ButtonSprite { get; private set; }
    private static Dictionary<int, Button> GameButtons;

    public static void Initialize(Button music, Button sound, Image disable, Image select, 
        List<Button> buttons, Dictionary<int, Button> gameButtons)
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
        GameButtons = gameButtons;

        Select = select;
        foreach (var b in buttons)
        {
            if (b.name == "Yellow")
            {
                b.onClick.AddListener(() => OnButtonClick(ButtonImageTypes.Yellow));
                Buttons.Add(ButtonImageTypes.Yellow, b);
            }
            else if (b.name == "Blue")
            {
                b.onClick.AddListener(() => OnButtonClick(ButtonImageTypes.Blue));
                Buttons.Add(ButtonImageTypes.Blue, b);
            }
            else if (b.name == "Purple")
            {
                b.onClick.AddListener(() => OnButtonClick(ButtonImageTypes.Purple));
                Buttons.Add(ButtonImageTypes.Purple, b);
            }
            else if (b.name == "Brawn")
            {
                b.onClick.AddListener(() => OnButtonClick(ButtonImageTypes.Brawn));
                Buttons.Add(ButtonImageTypes.Brawn, b);
            }
            else if (b.name == "Green")
            {
                b.onClick.AddListener(() => OnButtonClick(ButtonImageTypes.Green));
                Buttons.Add(ButtonImageTypes.Green, b);
            }
        }
        SetButtonSprite();
    }

    private static void OnButtonClick(ButtonImageTypes types)
    {
        PlayerPrefs.SetInt(ButtonSpriteKey, (int)types);
        SetButtonSprite();
    }

    private static void SetButtonSprite()
    {
        var v = (ButtonImageTypes)PlayerPrefs.GetInt(ButtonSpriteKey, 0);
        ButtonSprite = Buttons[v].GetComponent<Image>().sprite;
        Select.transform.position = Buttons[v].transform.position;

        foreach(var b in GameButtons.Values)
        {
            b.GetComponent<Image>().sprite = ButtonSprite;
        }
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