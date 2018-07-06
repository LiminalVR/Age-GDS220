using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

    [SerializeField] private Animator _campFireAnim;

    public override void Interact()
    {
        if(!_isActive)
        {
            // 1st interaction actions.
            _campFireAnim.SetBool("cFireDie", false);
            _campFireAnim.SetBool("cFireAlive", true);
        }
        else
        {
            // Other interaction actions.
            Debug.Log("Dig em 2");
        }

        base.Interact();
    }
}
