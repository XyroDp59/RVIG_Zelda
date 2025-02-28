using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool _launched = false;
    private bool _ready = false;

    private Vector3 _previousPos;

    public void OnRelease()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_launched)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _previousPos);
            _previousPos = transform.position;
        }
    }
}
