using UnityEngine;

public class Sword : MonoBehaviour
{
    public static bool swordActive;

    public void OnSwordGrabbed()
    {
        swordActive = true;
    }

    public void OnSwordReleased()
    {
        swordActive = false;
    }
}
