using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

    [SerializeField] private Animator _campFireAnim;

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            _campFireAnim.SetBool("cFireDie", false);
            _campFireAnim.SetBool("cFireAlive", true);
        }
        else
        {

        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {
            _campFireAnim.SetBool("cFireAlive", false);
            _campFireAnim.SetBool("cFireDie", true);
        }
        else
        {

        }
    }
}
