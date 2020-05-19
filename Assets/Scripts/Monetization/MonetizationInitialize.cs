using UnityEngine;
using UnityEngine.Advertisements;


public class MonetizationInitialize : MonoBehaviour
{
    //Google Play Store ID
    private readonly string _gameId = "3608975";
    private readonly bool _testMode = true;


    private void Start()
    {
        Advertisement.Initialize(_gameId, _testMode);
    }
}
