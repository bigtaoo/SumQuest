public static class Config
{
    public static int Width { get; set; }
    public static int Height { get; set; }
    public static int Target { get; set; }

    public static int Index(int i, int j)
    {
        return j * 100 + i;
    }
}