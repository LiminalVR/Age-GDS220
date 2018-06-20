using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        if(!_isActive)
        {
            Debug.Log("Spray em");
        }
        else
        {
            Debug.Log("Spray em 2");
        }
    }
}
