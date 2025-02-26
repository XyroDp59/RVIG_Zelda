using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform controllerTransform;
    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshRenderer.enabled = false;
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;
        
        transform.SetParent(controllerTransform);
        
        var vector3 = transform.localPosition;
        vector3.y = - 0.07f;
        vector3.z = - 0.067f;
        transform.localPosition = vector3;

        var angles = transform.rotation.eulerAngles;
        angles.x += 40f;
        var rotation = transform.rotation;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;
    }
    
    void Update()
    {
        _meshRenderer.enabled = Sword.swordActive;
        _meshCollider.enabled = Sword.swordActive;
    }
}
