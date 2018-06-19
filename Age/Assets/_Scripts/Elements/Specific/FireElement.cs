using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Light em");
    }
}
