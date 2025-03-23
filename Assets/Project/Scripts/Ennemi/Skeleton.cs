using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private float attackStartUpDuration;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackLag;
    [SerializeField] private Damager damager;
    [SerializeField] private float stunDuration;
    private GameObject _player;
    private NavMeshAgent _agent;
    private AudioSource _source;

    private Animator animator;
    private int _deathTrigHash;
    private int _walkBoolHash;
    private int _attackTrigHash;
    private int _hitTrigHash;
    private Health _health;
    private bool _attackHitBoxActive;

    private enum State
    {
        Default,
        Attacking,
        Damaged,
        Ded
    }
    
    private State _state;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _deathTrigHash = Animator.StringToHash("Death");
        _walkBoolHash = Animator.StringToHash("Walking");
        _attackTrigHash = Animator.StringToHash("Attack");
        _hitTrigHash = Animator.StringToHash("Hit");
        _health = GetComponent<Health>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _state = State.Default;
        damager.blocked.AddListener(OnBlock);
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _health.Death.AddListener(Death);
    }

    private void OnBlock()
    {
        StartCoroutine(DamagedRoutine());
    }

    private void FixedUpdate()
    {
        damager.collider.enabled = _state == State.Attacking && _attackHitBoxActive;
        
        if(!isActive || !_player || _state != State.Default) return;
        //CustomDebugger.log(distance.ToString());
        
        Vector3 targetPosition = _player.transform.position;
        targetPosition.y = transform.position.y;
        
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance < _agent.stoppingDistance)
        {
            Debug.Log(_state);
            StartCoroutine(AttackRoutine());
        }
        else
        {
            _agent.SetDestination(targetPosition);
            animator.SetBool(_walkBoolHash, true);
        } 
    }

    private IEnumerator AttackRoutine()
    {
        Debug.Log("here");
        _state = State.Attacking;
        animator.SetBool(_walkBoolHash, false);
        animator.SetTrigger(_attackTrigHash);
        _agent.SetDestination(transform.position);

        yield return null;
        
        yield return new WaitForSeconds(attackStartUpDuration);
        _attackHitBoxActive = true;

        yield return new WaitForSeconds(attackDuration);
        _attackHitBoxActive = false;

        yield return new WaitForSeconds(attackLag);
        if(_state == State.Attacking) _state = State.Default;//permet de ne pas interrompre une animation de degats
    }

    private IEnumerator DamagedRoutine()
    {
        _attackHitBoxActive = false;
        if(_state == State.Attacking) StopCoroutine(AttackRoutine()); 
        //CustomDebugger.log("damaged routine");
        yield return null;//wait to see if death triggered
        if (_state != State.Ded)
        {
            _state = State.Damaged;
            animator.SetTrigger(_hitTrigHash);
            yield return new WaitForSeconds(stunDuration);
            _state = State.Default;
        }
    }

    private void Death()
    {
        animator.SetTrigger(_deathTrigHash);
        _source.Play();
        _state = State.Ded;
        Destroy(gameObject, 1f);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Damager otherDamager;
        if (other.gameObject.TryGetComponent<Damager>(out otherDamager))
        {
            if (otherDamager.team != damager.team && _state != State.Damaged && _state != State.Ded)
            {
                _health.addHealth(otherDamager.damage);
                //CustomDebugger.log(otherDamager.damage.ToString());
                StartCoroutine(DamagedRoutine());
            }
        }
    }
}
