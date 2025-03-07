using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRData : MonoBehaviour
{
    public static XRData Instance;
    
    public Transform leftHand;
    public Transform rightHand;
    public GameObject XRPlayer;
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
