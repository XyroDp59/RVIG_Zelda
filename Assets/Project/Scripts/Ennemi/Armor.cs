using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    private Vector3 _defaultLocalPosition;
    
    private void Start()
    {
        _defaultLocalPosition = transform.localPosition;
    }

    public void Break()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.localPosition = _defaultLocalPosition;
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
