using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] private RectTransform healthBarFill;
    private int currentHealth;
    [SerializeField] Armor armor;

    public UnityEvent Death;
    public UnityEvent Damaged;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void addHealth(int health)
    {
        if (armor && health <0) return;
        Damaged.Invoke();
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        //CustomDebugger.log($"Health changed to {currentHealth}");
        float f = (float)currentHealth / ((float)maxHealth);
        var max = healthBarFill.anchorMax;
        max.x = f;
        healthBarFill.anchorMax = max;
        if (currentHealth <= 0)
        {
            Death.Invoke();
        }
    }
}