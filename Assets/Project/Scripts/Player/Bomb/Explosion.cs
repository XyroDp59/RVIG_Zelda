using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int bombDamage;
    private void OnTriggerEnter(Collider other)
    {
        //CustomDebugger.log("Triggered :" + other.gameObject.name);
        Armor armor;
        if(other.TryGetComponent(out armor))
        {
            armor.Break();
            return;
        }

        Health health;
        if (other.TryGetComponent(out health))
        {
            health.addHealth(-bombDamage);
        }
    }
}
