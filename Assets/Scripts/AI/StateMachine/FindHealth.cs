using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BotUtility botUtility = animator.GetComponentInParent<BotUtility>();
        HealthPack target = botUtility.FindClosestHealth();
        if (!botUtility.NavigateTo(target))
            animator.SetTrigger("failed");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
