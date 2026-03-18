using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static int Target { get; private set; }
    public static int FirstNumber { get; private set; }
    public static int SecondNumber { get; private set; }
    public static float GameStartTime { get; private set; }
    public static int ButtonWidth { get; private set; }
    public static int ButtonHeight { get; private set; }
    public static int ButtonPadding { get; private set; }
    public static int NumberImageSize { get; private set; }
    public static int LeftNumberCount { get; set; }
    public static int Select { get; set; }

    private static readonly string TargetNumberKey = "TargetNumber";

    public static void Initialize()
    {
        Target = PlayerPrefs.GetInt(TargetNumberKey, 6);
        SetGameData();
    }

    public static void NextLevel()
    {
        Target++;
        if (Target >= 99)
        {
            Target = 99;
        }
        PlayerPrefs.SetInt(TargetNumberKey, Target);
        SetGameData();
    }

    public static int Index(int i, int j)
    {
        return j * 100 + i;
    }

    public static List<int> InitializeNumbers()
    {
        LeftNumberCount = Config.Width * Config.Height;
        var randomNumbers = new List<int>();
        for (int i = 0; i < LeftNumberCount; i+=2)
        {
            int a = Random.Range(1, Config.Target - 1);
            int b = Target - a;
            randomNumbers.Add(a);
            randomNumbers.Add(b);
        }
        Helper.Shuffle(randomNumbers);
        return randomNumbers;
    }

    public static void SetGameData()
    {
        Select = -1;
        ButtonPadding = 10;
        FirstNumber = Random.Range(1, Target - 1);
        SecondNumber = Target - FirstNumber;
        GameStartTime = Time.time;
        if (Target > 0 && Target <= 20)
        {
            Width = 5;
            Height = 6;
            ButtonHeight = 160;
            ButtonWidth = 160;
            NumberImageSize = 85;
        }
        else if (Target > 20 && Target <= 50)
        {
            Width = 6;
            Height = 7;
            ButtonHeight = 135;
            ButtonWidth = 135;
            NumberImageSize = 70;
        }
        else
        {
            Width = 7;
            Height = 8;
            ButtonHeight = 122;
            ButtonWidth = 122;
            NumberImageSize = 60;
            ButtonPadding = 5;
        }
    }
}