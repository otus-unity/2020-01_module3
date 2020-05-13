using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Planner.DomainLanguage.TraitBased;
using UnityEngine.AI.Planner.DomainLanguage.TraitBased;
using Generated.AI.Planner.StateRepresentation;
using DefaultNamespace;

public class PlannerBot : MonoBehaviour
{
    PlayerHP hp;
    GunAmmo ammo;
    BotUtility botUtility;
    TraitComponent traitComponent;

    void Awake()
    {
        botUtility = GetComponent<BotUtility>();
        hp = botUtility.GetComponent<PlayerHP>();
        ammo = botUtility.GetComponentInChildren<GunAmmo>();
        traitComponent = GetComponent<TraitComponent>();
    }

    void Update()
    {
        ITraitData agent = traitComponent.GetTraitData<Agent>();
        agent.SetValue("Health", hp.Health);
        agent.SetValue("Ammo", (float)ammo.Count);
        agent.SetValue("Navigating", botUtility.IsNavigating());
        agent.SetValue("DistanceToClosestEnemy", botUtility.GetDistanceToClosestEnemy());
    }

    public IEnumerator NavigateTo(GameObject target)
    {
        Debug.Log($"NavigateTo({target.name})");
        if (botUtility.NavigateTo(target.transform)) {
            do {
                yield return null;
            } while (botUtility.IsNavigating());
        }
    }

    public IEnumerator NavigateToEnemy()
    {
        Debug.Log("NavigateToEnemy");
        PlayerCollider target = botUtility.FindClosestPlayer();
        if (botUtility.NavigateTo(target)) {
            do {
                yield return null;
            } while (botUtility.IsNavigating() && botUtility.GetDistanceToClosestEnemy() >= 20.0f);
        }
    }

    public IEnumerator AttackEnemy()
    {
        Debug.Log("AttackEnemy");
        PlayerCollider target = botUtility.FindClosestPlayer();
        if (botUtility.BeginAttack(target)) {
            yield return new WaitForSeconds(1.0f);
            botUtility.EndAttack();
        }
    }
}
