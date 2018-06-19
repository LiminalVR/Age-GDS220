using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement : MonoBehaviour, IElement {

    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    protected AudioSource _as;

    public virtual void Interact()
    {
        if(_interactionSound != null)
            _as.PlayOneShot(_interactionSound);

        if(_interactionEffect != null)
            Instantiate(_interactionEffect, transform.position, transform.rotation);
    }
}
