using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Player : MonoBehaviour
{
    private Health health;
    
    private HapticImpulsePlayer _hapticImpulseLeft;
    private HapticImpulsePlayer _hapticImpulseRight;

    private void Start()
    {
        health = GetComponent<Health>();
        health.Damaged.AddListener(OnDamage);
        
        _hapticImpulseLeft = XRData.Instance.leftHand.GetComponent<HapticImpulsePlayer>();
        _hapticImpulseRight = XRData.Instance.rightHand.GetComponent<HapticImpulsePlayer>();
    }
    
    

    private void OnDamage()
    {
        StartCoroutine(PostProcessControl.Instance.DamageVignetteFlicker());

        _hapticImpulseLeft.SendHapticImpulse(1, 0.5f);
        _hapticImpulseRight.SendHapticImpulse(1, 0.5f);
    }
}
