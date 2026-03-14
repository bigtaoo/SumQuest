using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private Button InitialNumber;

    private int Width;
    private int Height;
    private int Target;
    private int Select;
    private Dictionary<int, int> Numbers = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Width = 5;
        Height = 6;
        Target = 6;
        Select = -1;
        var startPosition = InitialNumber.transform.position;

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

        var index = 0;
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
                var tempIndex = Index(i, j);
                button.onClick.AddListener(() => OnButtonClick(tempIndex));

                Numbers[Index(i, j)] = randomNumbers[index];
                ++index;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonClick(int index)
    {
        Debug.Log($"Click index: {index}");
    }

    private int Index(int i, int j)
    {
        return j * 100 + i;
    }
}
