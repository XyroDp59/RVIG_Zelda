using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    public int team;
    public int damage;
    public UnityEvent blocked;

    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onCollision;
    
    public Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Shield"))
        {
            blocked.Invoke();
        }
        
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.addHealth(-damage);
            onDamage.Invoke();
        }
        onCollision.Invoke();
    }
}
