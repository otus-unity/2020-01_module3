using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
//#if UNITY_IOS
//    private string gameId = "1486551";
//#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
//    private string gameId = "1486550";
//#endif
    private readonly string _gameId = "3608975";
    private Button _myButton;
    public string MyPlacementId = "boxFound";

    private void Start()
    {
        _myButton = GetComponent<Button>();

        _myButton.interactable = Advertisement.IsReady(MyPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (_myButton) _myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, true);
    }

    private void ShowRewardedVideo()
    {
        Advertisement.Show(MyPlacementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == MyPlacementId)
        {
            _myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
