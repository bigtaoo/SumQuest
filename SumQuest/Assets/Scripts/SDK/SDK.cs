using UnityEngine;

public static class SDK
{
    private static bool SDKInitialized { get; set; } = false;
    private static SDKInterface SDKInstance { get; set; }

    public static void Initialize()
    {
    #if UNITY_WEBGL
        SDKInstance = new CrazyGamesSDK();
    #elif UNITY_IOS
    #elif UNITY_ANDROID
    #else
    #endif
    }

    public static void SDKInitializedCallback()
    {
        SDKInitialized = true;
        Debug.Log("SDK initialized!");
    }
}