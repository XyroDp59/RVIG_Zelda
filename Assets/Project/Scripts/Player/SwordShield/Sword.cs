using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sword : MonoBehaviour
{
    private Transform defaultParent;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;
    public static bool swordActive;
    private bool _rightHanded;

    private void Start()
    {
        _defaultPosition = transform.localPosition;
        _defaultRotation = transform.localRotation;
        defaultParent = transform.parent;
        //swordActive = true;
        //SetPositionFromController(rightControllerTransform);
    }

    public void OnSwordGrabbed(SelectEnterEventArgs args)
    {
        swordActive = true;
        _rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        Shield.instance.SetPositionFromController(_rightHanded ? leftControllerTransform : rightControllerTransform);
        SetPositionFromController(_rightHanded ? rightControllerTransform : leftControllerTransform);
    }

    public void OnSwordReleased()
    {
        CustomDebugger.log("Sword released");
        swordActive = false;
        transform.SetParent(defaultParent);
        transform.localPosition = _defaultPosition;
        transform.localRotation = _defaultRotation;
    }

    private void Update()
    {
        if (!swordActive)
        {
            transform.localPosition = _defaultPosition;
            transform.localRotation = _defaultRotation;
        }

        else // I shouldn't have to run that each frame but some weird offset occurs otherwise
        {
            SetPositionFromController(_rightHanded ? rightControllerTransform : leftControllerTransform);
        }
    }

    private void SetPositionFromController(Transform controllerTransform)
    {
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;
        
        transform.SetParent(controllerTransform);
        
        var vector3 = transform.localPosition;//slightly offset the position
        vector3.y = 0.02f;
        vector3.z = - 0.02f;
        transform.localPosition = vector3;

        var angles = transform.localRotation.eulerAngles;//and rotation as well
        angles.x = -217f;
        var rotation = transform.localRotation;
        rotation.eulerAngles = angles;
        transform.localRotation = rotation;
    }
}
