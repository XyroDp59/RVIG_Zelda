using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileOffSet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform nose;
    
    private NavMeshAgent _agent;
    private AudioSource _injuredSound;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _injuredSound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
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
        Vector3 direction = ((target.position+Vector3.up) - startPos).normalized;
        Projectile projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity);
        projectile.Initialise(direction);
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }

    public void OnDamaged()
    {
        _injuredSound.Play();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     CustomDebugger.log(other.gameObject.name + "triggered");
    // }

    private void OnCollisionEnter(Collision collision)
    {
        //CustomDebugger.log(collision.gameObject.name + "collided");
        if (collision.collider.CompareTag("Weapon"))
        {
            CustomDebugger.log("ouch");
            OnDamaged();
            _rigidbody.useGravity = true;
            Destroy(gameObject, 2f);
        }
    }
}
