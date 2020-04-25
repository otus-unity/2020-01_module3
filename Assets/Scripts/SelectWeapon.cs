using Photon.Pun;
using UnityEngine;

public sealed class SelectWeapon : MonoBehaviourPunCallbacks
{
    public Gun Gun;
    public PlayerAnimation PlayerAnimation;

    private void Start()
    {
        Gun.SetActive(false);
        if (!photonView.IsMine)
        {
            Destroy(this);
            return;
        }

        Gun.SetPlayerAnimation(PlayerAnimation);
    }

    private void Update()
    {
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
