using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

    public override void Interact()
    {
        base.Interact();

        if(!_isActive)
        {
            Debug.Log("Light em");
        }
        else
        {
            Debug.Log("Light em 2");
        }
    }
}
