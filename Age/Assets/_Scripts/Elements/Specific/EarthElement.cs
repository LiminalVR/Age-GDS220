using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

    public override void Interact()
    {
        base.Interact();

        if(!_isActive)
        {
            Debug.Log("Dig em");
        }
        else
        {
            Debug.Log("Dig em 2");
        }
    }
}
