using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

    public override void Interact()
    {
        if(!_isActive)
        {
            // 1st interaction actions.

            Debug.Log("Light em");
        }
        else
        {
            // Other interaction actions.

            Debug.Log("Light em 2");
        }

        base.Interact();
    }
}
