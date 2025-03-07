using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] private RectTransform healthBar;
    private int currentHealth;
    [SerializeField] Armor armor;

    public UnityEvent Death;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void addHealth(int health)
    {
        if (armor && health <0) return;

        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        float f = (float)currentHealth / ((float)maxHealth);
        //healthBar.anchorMax = new Vector2(f, 1);
        if (currentHealth <= 0)
        {
            Death.Invoke();
        }
    }
}