using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElement : MonoBehaviour, IElement {

    [Header("Interaction")]
    [SerializeField] private float _confirmationDuration;
    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    [SerializeField] protected AudioClip _otherInteractionSound;
    [SerializeField] protected GameObject _otherInteractionEffect;

    protected AudioSource _as;
    protected bool _isActive = false;
    protected bool _isConfirming = false;
    private float _confirmationTime;

    public float ConfirmationTime
    {
        get { return _confirmationTime; }
        set
        {
            if(value == 0)
                _isConfirming = false;
            else
                _isConfirming = true;

            _confirmationTime = value;

            if(_confirmationTime > _confirmationDuration)
            {
                _confirmationTime = 0;
                _isConfirming = false;
                Interact();
            }

            Debug.Log(_confirmationTime);
        }
    }

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