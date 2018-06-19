using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Blow em");
    }
}
