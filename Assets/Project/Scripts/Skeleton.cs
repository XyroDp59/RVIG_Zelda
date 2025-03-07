using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private float attackDuration;
    private GameObject _player;
    private NavMeshAgent _agent;
    
    private Animator animator;
    private int _deathTrigHash;
    private int _walkBoolHash;
    private int _attackTrigHash;
    private Health _health;

    private enum State
    {
        Default,
        Attacking
    }
    
    private State _state;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _deathTrigHash = Animator.StringToHash("Death");
        _walkBoolHash = Animator.StringToHash("Walking");
        _attackTrigHash = Animator.StringToHash("Attack");
        _health = GetComponent<Health>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _state = State.Default;
    }

    private void Update()
    {
        if(!isActive || !_player || _state != State.Default) return;
        //CustomDebugger.log(distance.ToString());
        
        Vector3 targetPosition = _player.transform.position;
        targetPosition.y = transform.position.y;
        
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance < _agent.stoppingDistance)
        {
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
        _state = State.Attacking;
        animator.SetBool(_walkBoolHash, false);
        animator.SetTrigger(_attackTrigHash);
        _agent.SetDestination(transform.position);

        yield return new WaitForSeconds(attackDuration);
        
        _state = State.Default;
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
