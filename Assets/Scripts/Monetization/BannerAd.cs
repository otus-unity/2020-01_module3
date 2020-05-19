using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{

    public string GameId = "3608975";
    public string PlacementId = "wantBanner";
    public bool TestMode = true;

    private void Start()
    {
        Advertisement.Initialize(GameId, TestMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    private IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(PlacementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(PlacementId);
    }
}
