using UnityEngine;
using UnityEngine.Serialization;

public class Shield : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;
    public static Shield Instance;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
        Instance = this;
        _meshRenderer.enabled = false;
        _meshCollider.enabled = false;
    }

    public void SetPositionFromController(Transform controllerTransform)
    {
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;
        
        transform.SetParent(controllerTransform);
        
        var vector3 = transform.localPosition;
        vector3.y = - 0.07f;
        vector3.z = - 0.067f;
        transform.localPosition = vector3;

        var angles = transform.localRotation.eulerAngles;
        angles.x += 40f;
        var rotation = transform.localRotation;
        rotation.eulerAngles = angles;
        transform.localRotation = rotation;
    }
    
    void Update()
    {
        _meshRenderer.enabled = Sword.swordActive;
        _meshCollider.enabled = Sword.swordActive;
    }
}
