using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

namespace Project.Scripts
{
    public class WeaponManager : MonoBehaviour
    {
        private static bool _holdingWeapon;
        [SerializeField] private GameObject sword;
        
        [SerializeField]
        XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");
        
        void OnEnable()
        {
            m_GripInput?.EnableDirectActionIfModeUsed();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 6 && !_holdingWeapon /*&& m_GripInput.ReadValue() > 0.5f*/)
            {
                sword.transform.position = other.transform.position;
            }
        }
    }
}
