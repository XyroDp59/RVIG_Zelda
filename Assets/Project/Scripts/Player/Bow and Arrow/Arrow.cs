using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool _launched = false;
    private bool _ready = false;

    private Rigidbody _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void ApplyForce(float force)
    {
        _launched = true;
        
        _rb.useGravity = true;
        _rb.isKinematic = false;
        
        _rb.AddForce(transform.forward * force);
    }
    
    public void OnRelease()
    {
        
    }
}
