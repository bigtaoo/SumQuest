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

    private int Width;
    private int Height;
    private int Target;
    private int Select;
    private Dictionary<int, int> Numbers = new();
    private Dictionary<int, Button> Buttons = new();
    private Dictionary<ImageType, Image> Images = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Width = 5;
        Height = 6;
        Target = 6;
        Select = -1;
        InitialNumber.gameObject.SetActive(false);
        var startPosition = InitialNumber.transform.position;

        GameResult.gameObject.SetActive(false);

        var randomNumbers = InitializeNumbers();
        InitializeImages();

        var numberIndex = 0;
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if ((i == 1 || i == 2 || i == 3) && (j == 2 || j == 3))
                {
                    continue;
                }
                var button = GameObject.Instantiate(InitialNumber);
                button.transform.position = 
                    new Vector3(startPosition.x + 160 * i, startPosition.y - 160 * j, startPosition.z);
                button.transform.SetParent(InitialNumber.transform.parent);
                button.name = Index(i, j).ToString();
                button.gameObject.SetActive(true);
                var buttonIndex = Index(i, j);
                button.onClick.AddListener(() => OnButtonClick(buttonIndex));
                Buttons[buttonIndex] = button;

                var num = randomNumbers[numberIndex];
                Numbers[buttonIndex] = num;
                ++numberIndex;

                var numImage = GetNumberImage(num);
                numImage.transform.position = button.transform.position;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeImages()
    {
        foreach (var image in InitialImages)
        {
            image.gameObject.SetActive(false);
            switch (image.name)
            {
                case "0": Images[ImageType.Number0] = image; break;
                case "1": Images[ImageType.Number1] = image; break;
                case "2": Images[ImageType.Number2] = image; break;
                case "3": Images[ImageType.Number3] = image; break;
                case "4": Images[ImageType.Number4] = image; break;
                case "5": Images[ImageType.Number5] = image; break;
                case "6": Images[ImageType.Number6] = image; break;
                case "7": Images[ImageType.Number7] = image; break;
                case "8": Images[ImageType.Number8] = image; break;
                case "9": Images[ImageType.Number9] = image; break;
                case "Hide": Images[ImageType.Hide] = image; break;
                case "Select": Images[ImageType.Select] = image; break;
                default: break;
            }
        }
    }

    private Image GetNumberImage(int number)
    {
        if (number < 0 || number > 9)
        {
            return null;
        }
        var image =  number switch
        {
            0 => Images[ImageType.Number0],
            1 => Images[ImageType.Number1],
            2 => Images[ImageType.Number2],
            3 => Images[ImageType.Number3],
            4 => Images[ImageType.Number4],
            5 => Images[ImageType.Number5],
            6 => Images[ImageType.Number6],
            7 => Images[ImageType.Number7],
            8 => Images[ImageType.Number8],
            9 => Images[ImageType.Number9],
            _ => null,
        };
        var newImage = GameObject.Instantiate(image, image.transform.parent);
        newImage.gameObject.SetActive(true);
        newImage.raycastTarget = false;

        return newImage;
    }

    private List<int> InitializeNumbers()
    {
        int count = Width * Height - 6;
        var randomNumbers = new List<int>();
        for (int i = 0; i < count; i+=2)
        {
            int a = Random.Range(1, Target - 1);
            int b = Target - a;
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
            DrawSelect(index);
            return;
        }
        if (Select == index)
        {
            Select = -1;
            Images[ImageType.Select].gameObject.SetActive(false);
            return;
        }
        if (Numbers[Select] + Numbers[index] == Target)
        {
            Buttons[Select].gameObject.SetActive(false);
            Buttons[index].gameObject.SetActive(false);
            Select = -1;
            Images[ImageType.Select].gameObject.SetActive(false);
            InitialEffect.GetComponent<FrameAnimation>().Play();
            return;
        }
        Select = index;
        DrawSelect(index);
    }

    private void DrawSelect(int index)
    {
        var image = Images[ImageType.Select];
        image.gameObject.SetActive(true);
        var button = Buttons[index];
        image.transform.position = button.transform.position;
        image.raycastTarget = false;
        image.transform.SetAsLastSibling();
    }

    private int Index(int i, int j)
    {
        return j * 100 + i;
    }
}
