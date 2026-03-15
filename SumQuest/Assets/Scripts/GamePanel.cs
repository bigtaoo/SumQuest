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
    [SerializeField] private Button Next;

    private int Select;
    private int LeftNumberCount;
    private Dictionary<int, int> Numbers = new();
    private Dictionary<int, Button> Buttons = new();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Config.Width = 5;
        Config.Height = 6;
        Config.Target = 6;
        Select = -1;
        InitialNumber.gameObject.SetActive(false);      
        GameResult.gameObject.SetActive(false);
        Next.onClick.AddListener(() => NextLevel());
          
        GameNumbers.Initialize(InitialImages);
        Effect.Initialize(InitialEffect);

        DrawNumbers();
        var headImage = GameNumbers.GetNumberImage(Config.Target);
        InitialTarget.sprite = headImage.sprite;
        InitialTarget.SetNativeSize();
        headImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextLevel()
    {
        Config.Width = 5;
        Config.Height = 6;
        Config.Target++;
        Select = -1;
        DrawNumbers();
        var headImage = GameNumbers.GetNumberImage(Config.Target);
        InitialTarget.sprite = headImage.sprite;
        InitialTarget.SetNativeSize();
        headImage.gameObject.SetActive(false);
        GameResult.gameObject.SetActive(false);
    }

    private void DrawNumbers()
    {
        var startPosition = InitialNumber.transform.position;
        var randomNumbers = InitializeNumbers();
        var numberIndex = 0;
        for (int i = 0; i < Config.Width; ++i)
        {
            for (int j = 0; j < Config.Height; ++j)
            {
                var button = GameObject.Instantiate(InitialNumber);
                button.transform.position = 
                    new Vector3(startPosition.x + 160 * i, startPosition.y - 160 * j, startPosition.z);
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

    private List<int> InitializeNumbers()
    {
        LeftNumberCount = Config.Width * Config.Height;
        var randomNumbers = new List<int>();
        for (int i = 0; i < LeftNumberCount; i+=2)
        {
            int a = Random.Range(1, Config.Target - 1);
            int b = Config.Target - a;
            randomNumbers.Add(a);
            randomNumbers.Add(b);
        }
        Helper.Shuffle(randomNumbers);
        return randomNumbers;
    }

    private void OnButtonClick(int index)
    {
        Debug.Log($"Click index: {index}");

        if (Select == -1)
        {
            Select = index;
            GameNumbers.DrawSelect(index, Buttons[index].transform.position);
            return;
        }
        if (Select == index)
        {
            Select = -1;
            GameNumbers.HideSelect();
            return;
        }
        if (Numbers[Select] + Numbers[index] == Config.Target)
        {
            Buttons[Select].gameObject.SetActive(false);
            Buttons[index].gameObject.SetActive(false);
            GameNumbers.HideNumer(Select);
            GameNumbers.HideNumer(index);
            Effect.Play(Buttons[Select].transform.position, Buttons[index].transform.position);
            Select = -1;
            GameNumbers.HideSelect();           
            LeftNumberCount -= 2;
            if (LeftNumberCount <= 0)
            {
                GameResult.gameObject.SetActive(true);
            }
            return;
        }
        Select = index;
        GameNumbers.DrawSelect(index, Buttons[index].transform.position);
    }  
}
