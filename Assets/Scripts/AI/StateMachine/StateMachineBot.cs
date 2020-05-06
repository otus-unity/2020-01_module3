using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class StateMachineBot : MonoBehaviour
{
    Animator anim;
    PlayerHP hp;
    GunAmmo ammo;
    BotUtility botUtility;

    void Awake()
    {
        anim = GetComponent<Animator>();
        botUtility = GetComponentInParent<BotUtility>();
        hp = botUtility.GetComponent<PlayerHP>();
        ammo = botUtility.GetComponentInChildren<GunAmmo>();
    }

    void Update()
    {
        anim.SetFloat("health", hp.Health);
        anim.SetFloat("ammo", ammo.Count);
        anim.SetBool("navigating", botUtility.IsNavigating());
        anim.SetFloat("distanceToClosestEnemy", botUtility.GetDistanceToClosestEnemy());
    }
}
