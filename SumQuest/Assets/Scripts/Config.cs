using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static int Width { get; set; }
    public static int Height { get; set; }
    public static int Target { get; set; }
    public static int LeftNumberCount { get; set; }
    public static int Select { get; set; }

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
}