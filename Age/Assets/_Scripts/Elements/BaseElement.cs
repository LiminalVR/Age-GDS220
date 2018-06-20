using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement : MonoBehaviour, IElement {

    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    protected AudioSource _as;
    private bool _isActive = false;

    private void Start()
    {
    }

    public virtual void Interact()
    {
        DelegatesAndEvents.ElementActivated();
        _isActive = true;

        if(_interactionSound != null)
            _as.PlayOneShot(_interactionSound);

        if(_interactionEffect != null)
            Instantiate(_interactionEffect, transform.position, transform.rotation);
    }
}