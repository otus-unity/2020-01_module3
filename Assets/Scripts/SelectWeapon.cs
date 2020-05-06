using Photon.Pun;
using UnityEngine;

public sealed class SelectWeapon : MonoBehaviourPunCallbacks
{
    public Gun Gun;
    public PlayerAnimation PlayerAnimation;
    bool isBot;

    private void Start()
    {
        isBot = GetComponent<BotUtility>() != null;

        Gun.SetActive(isBot);
        if (!photonView.IsMine)
        {
            Destroy(this);
            return;
        }

        Gun.SetPlayerAnimation(PlayerAnimation);
    }

    private void Update()
    {
        if (isBot)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Gun.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Gun.SetActive(false);
        }
    }
}
