
using UnityEngine;
using DefaultNamespace;

public sealed class PlayerCollider : MonoBehaviour
{
    PlayerHP hp;
    GunAmmo ammo;

    void Awake()
    {
        hp = GetComponentInChildren<PlayerHP>();
        ammo = GetComponentInChildren<GunAmmo>();
    }

    void OnTriggerEnter(Collider other)
    {
        var healthPack = other.GetComponent<HealthPack>();
        if (healthPack) {
            hp.AddHealth(healthPack.Amount);
            PackRespawner.instance.Add(healthPack.gameObject);
        }

        var ammoPack = other.GetComponent<AmmoPack>();
        if (ammoPack) {
            ammo.Count += ammoPack.Amount;
            PackRespawner.instance.Add(ammoPack.gameObject);
        }
    }
}
