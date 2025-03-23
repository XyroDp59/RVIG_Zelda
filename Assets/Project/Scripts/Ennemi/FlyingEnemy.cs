using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileOffSet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform nose;
    
    // Sounds
    [SerializeField] private AudioClip injuredSound;
    [SerializeField] private AudioClip flap1;
    [SerializeField] private AudioClip flap2;
    
    
    private NavMeshAgent _agent;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private int _deathTrigHash;
    
    private IEnumerator _flapCoroutine;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _deathTrigHash = Animator.StringToHash("Death");
        _animator.enabled = false;
    }

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _agent.SetDestination(target.position);
        StartCoroutine(FireCoroutine());

        _flapCoroutine = FlapCoroutine();
        StartCoroutine(_flapCoroutine);
    }

    IEnumerator FlapCoroutine()
    {
        _animator.enabled = true;
        while (true)
        {
            _audioSource.PlayOneShot(flap1);
            yield return new WaitForSeconds(0.5f);
            _audioSource.PlayOneShot(flap2);
            yield return new WaitForSeconds(0.5f);
        }
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
        StopCoroutine(_flapCoroutine);
        _audioSource.PlayOneShot(injuredSound);
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
            StopCoroutine(FireCoroutine());
            _rigidbody.useGravity = true;
            _animator.SetTrigger(_deathTrigHash);
            Destroy(gameObject, 1.6f);
        }
    }
}
