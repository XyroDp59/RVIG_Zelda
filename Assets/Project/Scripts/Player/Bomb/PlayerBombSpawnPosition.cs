using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombSpawnPosition : MonoBehaviour
{
    [SerializeField] private float offsetFront = -0.3f;
    [SerializeField] private float offsetHeight = 0.3f;
    [SerializeField] private float preferedHeight = 0.8f;

    private void Start()
    {
        preferedHeight = transform.position.y;
    }
    // Update is called once per frame
    private void Update()
    {
        /*Transform head = Camera.main.transform;
        Vector3 horizontalDir = (head.forward - head.forward.y * Vector3.up ).normalized;
        transform.position = head.position + offsetFront * horizontalDir + offsetHeight * Vector3.up;*/
        //transform.position = transform.position + (preferedHeight - transform.position.y) * Vector3.up;
        if (transform.GetChild(0) != null) transform.GetChild(0).localPosition = Vector3.zero;
    }
}
