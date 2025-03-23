using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.Damaged.AddListener(OnDamage);
    }

    private void OnDamage()
    {
        StartCoroutine(PostProcessControl.Instance.DamageVignetteFlicker());
    }
}
