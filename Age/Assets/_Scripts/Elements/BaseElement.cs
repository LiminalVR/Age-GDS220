using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement : MonoBehaviour, IElement {

    [Header("Interaction")]
    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    [SerializeField] protected AudioClip _otherInteractionSound;
    [SerializeField] protected GameObject _otherInteractionEffect;

    protected AudioSource _as;
    private bool _isActive = false;

    private void Start()
    {
    }

    public virtual void Interact()
    {
        if(!_isActive)
        {
            DelegatesAndEvents.ElementActivated();
            _isActive = true;

            if(_interactionSound != null)
                _as.PlayOneShot(_interactionSound);

            if(_interactionEffect != null)
                Instantiate(_interactionEffect, transform.position, transform.rotation);
        }
        else
        {
            if(_otherInteractionSound != null)
                _as.PlayOneShot(_otherInteractionSound);

            if(_otherInteractionEffect != null)
                Instantiate(_otherInteractionEffect, transform.position, transform.rotation);
        }
    }
}