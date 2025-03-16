using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    public int team;
    public int damage;

    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onCollision;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.addHealth(-damage);
            onDamage.Invoke();
        }
        onCollision.Invoke();
    }
}
