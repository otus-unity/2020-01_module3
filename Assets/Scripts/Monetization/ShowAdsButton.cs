using UnityEngine;
using UnityEngine.UI;
public class ShowAdsButton : MonoBehaviour
{
    public Button AdsButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AdsButton.gameObject.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AdsButton.gameObject.SetActive(false);
        }
    }

    
}
