using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sword : MonoBehaviour
{
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
    }

    public void OnSwordGrabbed(SelectEnterEventArgs args)
    {
        swordActive = true;
        _rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        Shield.instance.SetPositionFromController(_rightHanded ? leftControllerTransform : rightControllerTransform);
    }

    public void OnSwordReleased()
    {
        swordActive = false;
        transform.localPosition = _defaultPosition;
        transform.localRotation = _defaultRotation;
    }

    private void FixedUpdate()
    {
        if (!swordActive)
        {
            transform.localPosition = _defaultPosition;
            transform.localRotation = _defaultRotation;
        }
    }
}
