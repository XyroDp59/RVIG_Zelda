using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowMovement : MonoBehaviour
{
    [SerializeField] private GameObject stringMiddle;
    [SerializeField] private Arrow arrowPrefab;

    public bool bowIsLeft;
    
    //The next 3 lines are duplicated code, too late to fix :(
    private Transform defaultParent;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    
    private bool _bowTaken;
    private bool _arrowActive;
    private bool _isStringTaken;
    private Arrow _currentArrow;
    private XRGrabInteractable _grabInteractable;
    private AudioSource _releaseSound;
    private IEnumerator _arrowCoroutine;


    public void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _releaseSound = GetComponent<AudioSource>();

        _arrowCoroutine = ArrowCoroutine();
    }

    private void Start()
    {
        _defaultPosition = transform.localPosition;
        _defaultRotation = transform.localRotation;
        defaultParent = transform.parent;
    }
    

    public void OnTaken(SelectEnterEventArgs args)
    {
        _bowTaken = true;
        bowIsLeft = args.interactorObject.transform.parent.CompareTag("Left");
        CreateArrow();
    }

    public void OnRelease()
    {
        //CustomDebugger.log("bow dropped");
        _bowTaken = false;
        _arrowActive = false;
        
        StopCoroutine(_arrowCoroutine);
        if (_currentArrow != null)
        {
            Destroy(_currentArrow.gameObject);
            _currentArrow = null;
        }
        
        transform.SetParent(defaultParent);
        transform.localPosition = _defaultPosition;
        transform.localRotation = _defaultRotation;
    }

    public void OnStringTaken()
    {
        if (!(_bowTaken && _arrowActive)) return;
        _isStringTaken = true;
        
        _currentArrow.transform.position = stringMiddle.transform.position;
        _currentArrow.transform.parent = stringMiddle.transform;
    }

    public void OnStringRelease()
    {
        if (!(_bowTaken && _arrowActive && _isStringTaken)) return;
        
        _isStringTaken = false;
        
        _currentArrow.transform.parent = XRData.Instance.XRPlayer.transform.parent;

        _currentArrow.transform.position = transform.position + transform.forward * 0.1f;
        _currentArrow.ApplyForce(1000);

        _arrowActive = false;
        _currentArrow = null;
        StartCoroutine(_arrowCoroutine);
        _releaseSound.Play();
    }

    IEnumerator ArrowCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        CreateArrow();
    }

    void CreateArrow()
    {
        _arrowActive = true;
        _currentArrow = Instantiate(arrowPrefab);
        
        if (bowIsLeft)
        {
            _currentArrow.transform.position = XRData.Instance.rightHand.position;
            _currentArrow.transform.rotation = XRData.Instance.rightHand.rotation;
            _currentArrow.transform.SetParent(XRData.Instance.rightHand.transform);
        }
        else
        {
            _currentArrow.transform.position = XRData.Instance.leftHand.position;
            _currentArrow.transform.rotation = XRData.Instance.leftHand.rotation;
            _currentArrow.transform.SetParent(XRData.Instance.leftHand);
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (_bowTaken)
        {
            if (bowIsLeft)
            {
                transform.position = XRData.Instance.leftHand.position;
                transform.rotation = XRData.Instance.leftHand.rotation;
            }
            else
            {
                transform.position = XRData.Instance.rightHand.position;
                transform.rotation = XRData.Instance.rightHand.rotation;
            }
        }

        if (_bowTaken && _arrowActive && _isStringTaken)
        {
            _currentArrow.transform.rotation = Quaternion.LookRotation(transform.position-stringMiddle.transform.position);
        }

        if (!_bowTaken)
        {
            if (_currentArrow != null)
            {
                Destroy(_currentArrow.gameObject);
                _currentArrow = null;
            }
        }
        
    }
}
