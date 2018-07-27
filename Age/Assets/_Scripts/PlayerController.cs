﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private string _buttonName;
    [SerializeField] private float _maxDis;
    [SerializeField] private float _mouseSensitivity = 15.0f;
    private IElement _selectedElement;

    private float _xRot;
    private float _yRot;
    private Quaternion _originalRot;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.positionCount = 2;
        _originalRot = transform.localRotation;
    }

    private void Update()
    {



        _xRot += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _yRot += Input.GetAxis("Mouse Y") * _mouseSensitivity;
        transform.localRotation = _originalRot * Quaternion.AngleAxis(_xRot, Vector3.up) * Quaternion.AngleAxis(_yRot, -Vector3.right);

       if(Input.GetButtonDown(_buttonName))
       {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit, _maxDis, _interactionLayers))
            {
                _selectedElement = raycastHit.collider.gameObject.GetComponent<IElement>();
            }
       }

        if(Input.GetButton(_buttonName))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit, _maxDis, _interactionLayers))
            {
                _lineRenderer.SetPosition(1, new Vector3(0, 0, raycastHit.distance));
            }

            if(_selectedElement != null)
            {
                _selectedElement.ConfirmationTime += Time.deltaTime;
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
}
