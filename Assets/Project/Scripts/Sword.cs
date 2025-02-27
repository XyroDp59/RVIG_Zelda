using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sword : MonoBehaviour
{
    public static bool swordActive;
    public static bool rightHanded;

    public void OnSwordGrabbed(SelectEnterEventArgs args)
    {
        swordActive = true;
        rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        Shield.Instance.SetPositionFromController(rightHanded ? Shield.Instance.leftControllerTransform : Shield.Instance.rightControllerTransform);
    }

    public void OnSwordReleased()
    {
        swordActive = false;
    }
}
