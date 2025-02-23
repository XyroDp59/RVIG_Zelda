using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BowString : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;


    // Update is called once per frame
    void Update()
    {
        float length = (endPos.position - startPos.position).magnitude/2;
        
        transform.localScale = new Vector3(transform.localScale.x,length,transform.localScale.z);
        
        transform.position = (endPos.position + startPos.position) / 2;
        
        transform.LookAt(endPos);
        transform.Rotate(Vector3.left, 90);
    }
}
