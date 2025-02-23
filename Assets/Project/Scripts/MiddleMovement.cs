using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMovement : MonoBehaviour
{
    [SerializeField] private Transform secondHand;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = secondHand.position;
    }
}
