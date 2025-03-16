using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileOffSet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform nose;
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.SetDestination(target.position);
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if ((target.position - transform.position).magnitude <= 2)
            {
                yield return new WaitForSeconds(1);
                
                Fire();
            }

            yield return null;
        }
    }

    void Fire()
    {
        Vector3 startPos = nose.position + transform.forward * projectileOffSet;
        Vector3 direction = (target.position - startPos).normalized;
        Projectile projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity);
        projectile.Initialise(direction);
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }
}
