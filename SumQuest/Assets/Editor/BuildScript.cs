using UnityEditor;

public class BuildScript
{
    static string[] scenes = { "Assets/Scenes/SumQuestScene.unity" };

    static void SetVersion()
    {
        // Get build number from GitHub
        string buildNumber = System.Environment.GetEnvironmentVariable("BUILD_NUMBER");

        int versionCode = 1;

        if (!string.IsNullOrEmpty(buildNumber))
        {
            int.TryParse(buildNumber, out versionCode);
        }

        // Set Android versionCode
        PlayerSettings.Android.bundleVersionCode = versionCode;

        // Optional: versionName
        PlayerSettings.bundleVersion = "1.0." + versionCode;

        UnityEngine.Debug.Log("VersionCode set to: " + versionCode);
    }
    
    public static void BuildAAB()
    {
        SetVersion();
        EditorUserBuildSettings.buildAppBundle = true;
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "build/Android/sumquest.aab",
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(options);
    }

    public static void BuildAPK()
    {
        SetVersion();
        EditorUserBuildSettings.buildAppBundle = false;

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "build/Android/sumquest.apk",
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(options);
    }
}
