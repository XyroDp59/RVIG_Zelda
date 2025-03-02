using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private Animator animator;
    private int _deathTrigHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _deathTrigHash = Animator.StringToHash("Death");
    }
    
    private void OnCollisionEnter(Collision other)
    {
        CustomDebugger.log("bone");
        Debug.Log("bone");
        if (other.gameObject.CompareTag("Weapon"))
        {
            animator.SetTrigger(_deathTrigHash);
        }
    }
}
