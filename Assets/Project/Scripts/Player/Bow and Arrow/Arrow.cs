using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    
    public void ApplyForce(float force)
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        
        _rb.AddForce(transform.forward * force);
        _collider.enabled = true;
    }

    public void OnDamage()
    {
        Destroy(gameObject);
    }
}
