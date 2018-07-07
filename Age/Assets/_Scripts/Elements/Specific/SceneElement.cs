using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : BaseElement
{
    private SeasonManager _seasonManager;

    private void Start()
    {
        _seasonManager = GameObject.FindGameObjectWithTag("GameGod").GetComponent<SeasonManager>();
    }

    public override void Interact()
    {
        base.Interact();

        StartCoroutine(_seasonManager.ChangeSeason());
    }

    protected override void EnactSummerActions(bool initialAction)
    {
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
    }

    protected override void EnactWinterActions(bool initialAction)
    {
    }

    protected override void EnactSpringActions(bool initialAction)
    {
    }
}
