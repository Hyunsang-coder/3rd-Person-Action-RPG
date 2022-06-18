using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    Collider[] colliders;
    Rigidbody[] rigidbodies;
    
    void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies = GetComponentsInChildren<Rigidbody>(true);

        RagDollToggle(false);
    }

    public void RagDollToggle(bool isRagdoll)
    {
        animator.enabled = !isRagdoll;

        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
            
        }
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if(rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        controller.enabled = !isRagdoll;
    }
}
