using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public void Break()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Sword s;
        if (collision.gameObject.TryGetComponent(out s))
        {
            //s.Parry();
            CustomDebugger.log("bonk");
        }
    }

}
