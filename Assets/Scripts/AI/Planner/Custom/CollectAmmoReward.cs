using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.BotPlan;
using Unity.AI.Planner.DomainLanguage.TraitBased;

public struct CollectAmmoReward : ICustomTraitReward<Agent>
{
    public float RewardModifier(Agent agent)
    {
        return (agent.Ammo < 5.0f ? 10.0f : 0.0f);
    }
}
