using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.SetDestination(target.position);
        
    }

    IEnumerator FireCoroutine()
    {
        if ((target.position - transform.position).magnitude > 2.1) yield return null;
        
        yield return new WaitForSeconds(1f);

        Fire();
    }

    void Fire()
    {
        
    }
}
