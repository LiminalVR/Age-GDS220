using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        if(!_isActive)
        {
            Debug.Log("Blow em");
        }
        else
        {
            Debug.Log("Blow em 2");
        }
    }
}
