using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

    public override void Interact()
    {
        if(!_isActive)
        {
            // 1st interaction actions.

            Debug.Log("Blow em");
        }
        else
        {
            // Other interaction actions.

            Debug.Log("Blow em 2");
        }

        base.Interact();
    }
}
