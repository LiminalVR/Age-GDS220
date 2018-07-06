using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

    public override void Interact()
    {
        if(!_isActive)
        {
            // 1st interaction actions.

            Debug.Log("Spray em");
        }
        else
        {
            // Other interaction actions.

            Debug.Log("Spray em 2");
        }

        base.Interact();
    }
}
