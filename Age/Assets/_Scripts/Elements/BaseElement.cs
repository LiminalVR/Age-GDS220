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

    [Header("Fade")]
    [SerializeField] private Renderer[] _renderersToFade;
    [SerializeField] private float _targetAlpha;
    [SerializeField] private float _fadeDuration;

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
        StartCoroutine(AnimateEffect(_isActive));
        _isActive = true;
    }

    private IEnumerator AnimateEffect(bool otherEffect)
    {
        GameObject activeEffect = null;

        if(!otherEffect)
        {
            DelegatesAndEvents.ElementActivated();

            if(_interactionSound != null)
                _as.PlayOneShot(_interactionSound);

            if(_interactionEffect != null)
                activeEffect = Instantiate(_interactionEffect, transform.position, transform.rotation);

            Debug.Log(activeEffect);

            while(activeEffect != null)
            {
                yield return null;
            }

            StartCoroutine(Fade());
        }
        else
        {
            if(_otherInteractionSound != null)
                _as.PlayOneShot(_otherInteractionSound);

            if(_otherInteractionEffect != null)
                activeEffect = Instantiate(_otherInteractionEffect, transform.position, transform.rotation);
        }

        yield return null;
    }

    private IEnumerator Fade()
    {
        List<Color> _startColours = new List<Color>();

        foreach(Renderer rend in _renderersToFade)
        {
            _startColours.Add(rend.material.color);
        }

        var step = 0.0f;

        while(step < 1)
        {
            step += Time.deltaTime / _fadeDuration;

            for(int index = 0; index < _startColours.Count; index++)
            {
                Material mat = _renderersToFade[index].material;
                Color startColour = _startColours[index];

                mat.color = Color.Lerp(startColour, new Color(mat.color.r, mat.color.g, mat.color.b, _targetAlpha), step);
                _renderersToFade[index].material = mat;
            }

            yield return null;
        }

        yield return null;
    }
}