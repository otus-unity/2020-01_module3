using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.BotPlan;
using Unity.AI.Planner.DomainLanguage.TraitBased;

public struct CollectHealthReward : ICustomTraitReward<Agent>
{
    public float RewardModifier(Agent agent)
    {
        return (agent.Health < 50.0f ? 10.0f : 0.0f);
    }
}
