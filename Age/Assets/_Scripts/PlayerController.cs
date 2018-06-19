using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private string _buttonName;
    [SerializeField] private float _maxDis;

    private void Update()
    {
        if(Input.GetButtonDown(_buttonName))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit, _maxDis, _interactionLayers))
            {

                IElement selectedElement = raycastHit.collider.gameObject.GetComponent<IElement>();

                if(selectedElement != null)
                    selectedElement.Interact();
            }
        }
    }
}
