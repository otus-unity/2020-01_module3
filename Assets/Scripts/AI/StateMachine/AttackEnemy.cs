using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : StateMachineBehaviour
{
    bool didAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BotUtility botUtility = animator.GetComponentInParent<BotUtility>();
        PlayerCollider target = botUtility.FindClosestPlayer();
        didAttack = botUtility.BeginAttack(target);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (didAttack) {
            BotUtility botUtility = animator.GetComponentInParent<BotUtility>();
            botUtility.EndAttack();
        }
    }
}
