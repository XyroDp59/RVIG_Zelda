using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BowMovement : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [SerializeField] private GameObject stringMiddle;
    
    [SerializeField] private Arrow arrowPrefab;

    private bool _bowIsLeft;
    private bool _bowTaken;
    private bool _arrowActive;
    private bool _isStringTaken;

    private Arrow _currentArrow;
    
    
    public void OnTaken(SelectEnterEventArgs args)
    {
        _bowTaken = true;
        _bowIsLeft = args.interactorObject.transform.parent.CompareTag("Left");
    }

    public void OnRelease()
    {
        _bowTaken = false;
    }

    public void OnStringTaken()
    {
        if (!_bowTaken) return;

        _currentArrow.transform.parent = stringMiddle.transform;
    }

    public void OnStringRelease()
    {
        if (!_bowTaken || !_arrowActive || !_isStringTaken) return; 
        
        
        _currentArrow.transform.parent = transform.parent;
        _currentArrow.ApplyForce(10);
        
    }

    void CreateArrow()
    {
        _arrowActive = true;
        _currentArrow = Instantiate(arrowPrefab);
        if (_bowIsLeft)
        {
            _currentArrow.transform.SetParent(rightHand);
        }
        else
        {
            _currentArrow.transform.SetParent(leftHand);
        }
        CustomDebugger.log("String taken");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (_bowTaken && !_arrowActive)
        {
            CreateArrow();
        }

        if (_bowTaken && _arrowActive & _isStringTaken)
        {
            _currentArrow.transform.rotation = Quaternion.LookRotation(transform.position-stringMiddle.transform.position);
        }
        
    }
}
