using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMovement : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;

    private bool _taken = false;
    private bool _bowTaken;
    
    public void OnRelease()
    {
        _taken = false;
    }
    
    public void OnHoven()
    {
        _taken = _bowTaken;
    }

    public void OnBowTaken()
    {
        _bowTaken = true;
    }

    public void OnBowRelease()
    {
        _bowTaken = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_taken)
        {
            transform.position = (up.position + down.position)/2;
        }
        else
        {
            
        }
    }
}
