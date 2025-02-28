using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MiddleMovement : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;

    private bool _taken;
    private bool _bowTaken;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    { 
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.enabled = false;
    }
    
    public void OnRelease()
    {
        _taken = false;
    }
    
    public void OnTaken()
    {
        _taken = _bowTaken;
    }

    public void OnBowTaken()
    {
        _bowTaken = true;
        _grabInteractable.enabled = true;
    }

    public void OnBowRelease()
    {
        _bowTaken = false;
        _taken = false;
        
        _grabInteractable.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_taken)
        {
            transform.position = (up.position + down.position)/2;
        }
    }
}
