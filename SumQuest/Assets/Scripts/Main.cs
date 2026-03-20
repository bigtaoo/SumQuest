using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private Button InitialNumber;
    [SerializeField] private Image InitialTarget;
    [SerializeField] private GameObject InitialEffect;
    [SerializeField] private List<Image> InitialImages;
    [SerializeField] private GameObject GameResult;
    [SerializeField] private Button NextButton;
    [SerializeField] private Button RetryButton;
    [SerializeField] private GameObject SettingsUI;

    [SerializeField] private Image FirstNumber;
    [SerializeField] private Image SecondNumber;
    [SerializeField] private Image TimeCount;
    [SerializeField] private Button SettingsButton;

    [SerializeField] private Button MusicButton;
    [SerializeField] private Button SoundButton;
    [SerializeField] private Image DisableImage;
    [SerializeField] private Image SettingsSelectImage;
    [SerializeField] private List<Button> SettingsButtons;
    
    private Dictionary<int, int> Numbers = new();
    private Dictionary<int, Button> Buttons = new();
    private bool IsGameEnd = false;
    private int LastScreenWidth;
    private int LastScreenHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resize();
        Config.Initialize();
        InitialNumber.gameObject.SetActive(false);      
        GameResult.SetActive(false);
        NextButton.onClick.AddListener(() => NextLevel());
        RetryButton.onClick.AddListener(() => Retry());
        SettingsUI.gameObject.SetActive(false);
        SettingsButton.onClick.AddListener(() => SettingsUI.SetActive(!SettingsUI.activeSelf));
          
        GameNumbers.Initialize(InitialImages);
        Effect.Initialize(InitialEffect);
        Settings.Initialize(MusicButton, SoundButton, DisableImage, SettingsSelectImage, 
            SettingsButtons, Buttons);

        DrawNumbers();
        Header.Initialize(InitialTarget, FirstNumber, SecondNumber, TimeCount, SettingsButton);

        StartCoroutine(RecheckSoundSettings());
    }

    IEnumerator RecheckSoundSettings()
    {
        yield return new WaitForSeconds(0.5f);
        Settings.SetDisableImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != LastScreenWidth || Screen.height != LastScreenHeight)
        {
            Resize();
        }

        if (IsGameEnd)
        {
            return;
        }
        Header.UpdateTimeCount();

        if (Header.TimeLeft < 0)
        {
            GameResult.gameObject.SetActive(true);
            RetryButton.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(false);
        }
    }
    
    private void Resize()
    {
        Canvas.ForceUpdateCanvases();
        LastScreenWidth = Screen.width;
        LastScreenHeight = Screen.height;

        var canvas = GetComponentInParent<Canvas>();
        Debug.Log($"Canvas scale: {canvas.scaleFactor}");
        Config.SetCanvasScale(canvas.scaleFactor);
    }

    private void NextLevel()
    {
        IsGameEnd = false;
        Config.NextLevel();
        DrawNumbers();
        Header.UpdateHeader();
        GameResult.gameObject.SetActive(false);
    }

    private void Retry()
    {
        IsGameEnd = false;
        GameNumbers.HideAllNumbers();
        foreach (var b in Buttons.Values)
        {
            b.gameObject.SetActive(false);
            b.onClick.RemoveAllListeners();
        }
        Config.SetGameData();
        DrawNumbers();
        Header.UpdateHeader();
        GameResult.gameObject.SetActive(false);
    }

    private void DrawNumbers()
    {
        // Debug.Log($"Draw number, canvas scale: {Config.CanvasScale}");
        var startPosition = InitialNumber.transform.position;
        var randomNumbers = Config.InitializeNumbers();
        var numberIndex = 0;
        for (int i = 0; i < Config.Width; ++i)
        {
            for (int j = 0; j < Config.Height; ++j)
            {
                var button = GetButtonObject();
                button.GetComponent<Image>().sprite = Settings.ButtonSprite;
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Config.ButtonWidth * Config.CanvasScale, Config.ButtonHeight * Config.CanvasScale);
                button.transform.position = 
                    new Vector3(startPosition.x + (Config.ButtonWidth + Config.ButtonPadding) * Config.CanvasScale * i, 
                    startPosition.y - (Config.ButtonHeight + Config.ButtonPadding) * Config.CanvasScale * j, startPosition.z);
                button.transform.SetParent(InitialNumber.transform.parent);
                button.name = Config.Index(i, j).ToString();
                button.gameObject.SetActive(true);
                var buttonIndex = Config.Index(i, j);
                button.onClick.AddListener(() => OnButtonClick(buttonIndex));
                Buttons[buttonIndex] = button;

                var num = randomNumbers[numberIndex];
                Numbers[buttonIndex] = num;
                ++numberIndex;

                GameNumbers.DrawNumber(buttonIndex, num, button.transform.position);
            }
        }
    }

    private Button GetButtonObject()
    {
        foreach (var o in Buttons.Values)
        {
            if (!o.IsActive())
            {
                return o;
            }
        }
        return GameObject.Instantiate(InitialNumber);
    }

    private void OnButtonClick(int index)
    {
        // Debug.Log($"Click index: {index}");

        SoundManager.Instance.PlayButtonClick();

        if (Config.Select == -1)
        {
            Config.Select = index;
            GameNumbers.DrawSelect(index, Buttons[index].transform.position);
            return;
        }
        if (Config.Select == index)
        {
            Config.Select = -1;
            GameNumbers.HideSelect();
            return;
        }
        if (Numbers[Config.Select] + Numbers[index] == Config.Target)
        {
            SoundManager.Instance.PlayNumberMatch();

            Buttons[Config.Select].gameObject.SetActive(false);
            Buttons[Config.Select].onClick.RemoveAllListeners();
            Buttons[index].gameObject.SetActive(false);
            Buttons[index].onClick.RemoveAllListeners();
            GameNumbers.HideNumer(Config.Select);
            GameNumbers.HideNumer(index);
            Effect.Play(Buttons[Config.Select].transform.position, Buttons[index].transform.position);
            Config.Select = -1;
            GameNumbers.HideSelect();           
            Config.LeftNumberCount -= 2;
            if (Config.LeftNumberCount <= 0)
            {
                IsGameEnd = true;
                GameResult.gameObject.SetActive(true);
                NextButton.gameObject.SetActive(true);
                RetryButton.gameObject.SetActive(false);
            }
            return;
        }
        Config.Select = index;
        GameNumbers.DrawSelect(index, Buttons[index].transform.position);
    }  
}
