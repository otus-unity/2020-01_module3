using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    Animator animator;
    public Transform LeftToeBase;
    public Transform RightToeBase;
    public float MaxDistanceFromGround;
    public float MaxDistanceToGround;
    public LayerMask layerMask;

    public Transform LookAtTarget;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator == null)
            return;

        /*
        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(LookAtTarget.position);
        */

        FootIK(AvatarIKGoal.LeftFoot, LeftToeBase, "IKLeftFootWeight");
        FootIK(AvatarIKGoal.RightFoot, RightToeBase, "IKRightFootWeight");
    }

    void FootIK(AvatarIKGoal goal, Transform toeBase, string weightName)
    {
        float weight = animator.GetFloat(weightName);
        animator.SetIKPositionWeight(goal, weight);
        animator.SetIKRotationWeight(goal, weight);

        Vector3 footPosition = animator.GetIKPosition(goal);
        float yOffset = footPosition.y - toeBase.position.y;

        RaycastHit hit;
        Ray ray = new Ray(footPosition + Vector3.up * MaxDistanceFromGround, Vector3.down);
        if (Physics.Raycast(ray, out hit, MaxDistanceFromGround + MaxDistanceToGround, layerMask)) {
            Vector3 newFootPosition = hit.point;
            newFootPosition.y += yOffset;
            animator.SetIKPosition(goal, newFootPosition);
            animator.SetIKRotation(goal, Quaternion.LookRotation(transform.forward, hit.normal));
        }
    }
}
