using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private string _buttonName;
    [SerializeField] private float _maxDis;
    private IElement _selectedElement;

    private void Update()
    {
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
