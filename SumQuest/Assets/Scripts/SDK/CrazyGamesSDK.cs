#if UNITY_WEBGL
public class CrazyGamesSDK : SDKInterface
{
    public void Initialize()
    {
        CrazyGames.CrazySDK.Init(() => {SDK.SDKInitializedCallback(); });
    }

    public void LoadBanner()
    {
        
    }

    public void DisplayBanner()
    {
       
    }
}
#endif