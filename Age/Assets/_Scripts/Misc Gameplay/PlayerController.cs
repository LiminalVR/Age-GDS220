using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using UnityEngine;
using Liminal.SDK.VR.Input;

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameObject _pointer;
    [SerializeField] private GameObject _controller;
    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private string _buttonName;
    [SerializeField] private float _maxDis;
    [SerializeField] private float _mouseSensitivity = 15.0f;
    private BaseElement _selectedElement;

    [SerializeField] private bool _isVR;

    private float _xRot;
    private float _yRot;
    private Quaternion _originalRot;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = _controller.GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.positionCount = 2;
        
        _originalRot = transform.localRotation;
    }

    private void Update()
    {
        Ray ray = new Ray(_controller.transform.position, _controller.transform.forward);
        RaycastHit raycastHit;

        if(Physics.Raycast(ray, out raycastHit, _maxDis, _interactionLayers))
        {
            _lineRenderer.SetPosition(1, new Vector3(0, 0, raycastHit.distance));
        }

        _pointer.transform.position = raycastHit.point;
        //_pointer.transform.rotation = Quaternion.Euler(0, OVRInput.GetLocalControllerRotation(OVRInput.Controller.Active).eulerAngles.y, 0);


        if(!_isVR)
        {
            _xRot += Input.GetAxis("Mouse X") * _mouseSensitivity;
            _yRot += Input.GetAxis("Mouse Y") * _mouseSensitivity;
            transform.localRotation = _originalRot * Quaternion.AngleAxis(_xRot, Vector3.up) * Quaternion.AngleAxis(_yRot, -Vector3.right);

            if(Input.GetButtonDown(_buttonName))
            {
                _selectedElement = raycastHit.collider.gameObject.GetComponent<BaseElement>();
            }

            if(Input.GetButton(_buttonName))
            {
                if(_selectedElement != null)
                {
                    _selectedElement.ConfirmationTime += Time.deltaTime;
                    print(_selectedElement.ConfirmationTime);
                }
            }

            if(Input.GetButtonUp(_buttonName))
            {
                if(_selectedElement != null)
                {
                    _selectedElement.ConfirmationTime = 0;
                    _selectedElement = null;
                }
            }
        }
        else
        {
            if(Input.GetButtonDown(VRButton.One) || OVRInput.GetDown(OVRInput.Button.One) || Input.GetButtonDown(VRButton.Trigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {

                _selectedElement = raycastHit.collider.gameObject.GetComponent<BaseElement>();
            }

            if(Input.GetButton(VRButton.One) || OVRInput.Get(OVRInput.Button.One) || Input.GetButton(VRButton.Trigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                if(_selectedElement != null)
                {
                    _selectedElement.ConfirmationTime += Time.deltaTime;
                }
            }

            if(Input.GetButtonUp(VRButton.One) || OVRInput.GetUp(OVRInput.Button.One) || Input.GetButtonUp(VRButton.Trigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                if(_selectedElement != null)
                {
                    _selectedElement.ConfirmationTime = 0;
                    _selectedElement = null;
                }
            }
        }
    }
}
