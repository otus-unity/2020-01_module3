using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public sealed class Messenger : MonoBehaviour, IOnEventCallback
{
    public Text Text;
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            string data = (string)photonEvent.CustomData;
            Text.text = $"Умер {data}";
        }
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
