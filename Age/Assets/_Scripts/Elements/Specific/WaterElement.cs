using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Spray em");
    }
}
