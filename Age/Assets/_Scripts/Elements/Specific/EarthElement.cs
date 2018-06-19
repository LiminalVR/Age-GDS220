using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Dig em");
    }
}
