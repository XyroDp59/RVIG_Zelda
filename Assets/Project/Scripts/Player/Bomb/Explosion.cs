using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CustomDebugger.log("Triggered");
        Armor armor;
         
        if(other.TryGetComponent(out armor))
        {
            armor.Break();
        }
    }
}
