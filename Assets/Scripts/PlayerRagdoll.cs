using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DefaultNamespace;

public class PlayerRagdoll : MonoBehaviour
{
    private Animator animator;
    private PhotonAnimatorView animatorView;
    private Collider collider;
    private NavMeshAgent navMeshAgent;
    private PlayerHP hp;
    private Rigidbody[] rigidBodies;

    void Start()
    {
        animator = GetComponent<Animator>();
        animatorView = GetComponent<PhotonAnimatorView>();
        collider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = GetComponent<PlayerHP>();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in rigidBodies)
            body.isKinematic = true;
    }

    void Update()
    {
        if (hp.Health <= 0) {
            Destroy(collider);
            Destroy(animatorView);
            Destroy(animator);
            Destroy(navMeshAgent);
            foreach (Rigidbody body in rigidBodies)
                body.isKinematic = false;
            Destroy(this);
        }
    }
}
