using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static int Target { get; private set; }
    public static int ButtonWidth { get; private set; }
    public static int ButtonHeight { get; private set; }
    public static int ButtonPadding { get; private set; }
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
            int b = Config.Target - a;
            randomNumbers.Add(a);
            randomNumbers.Add(b);
        }
        Helper.Shuffle(randomNumbers);
        return randomNumbers;
    }

    private static void SetGameData()
    {
        Width = 5;
        Height = 6;
        Select = -1;
        ButtonHeight = 150;
        ButtonWidth = 150;
        ButtonPadding = 10;
    }
}