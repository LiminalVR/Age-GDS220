using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElement : MonoBehaviour, IElement {

    [Header("Interaction")]
    [SerializeField] private float _confirmationDuration;
    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    [SerializeField] protected AudioClip _otherInteractionSound;
    [SerializeField] protected GameObject _otherInteractionEffect;
    private GameObject _startObj;

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
        _startObj = this.gameObject;
    }

    private void ResetElement()
    {
        var temp = Instantiate(_startObj, transform.position, transform.rotation);
        temp.SetActive(false);
        Destroy(this.gameObject);
    }

    public virtual void Interact()
    {
        switch(SeasonManager._currentSeason)
        {
            case SeasonManager.Seasons.SUMMER:
                EnactSummerActions(!_isActive);
                break;

            case SeasonManager.Seasons.AUTUMN:
                EnactAutumnActions(!_isActive);
                break;

            case SeasonManager.Seasons.WINTER:
                EnactWinterActions(!_isActive);
                break;

            case SeasonManager.Seasons.SPRING:
                EnactSpringActions(!_isActive);
                break;

            default:
                Debug.LogError("Unknown Season");
                break;
        }

        StartCoroutine(AnimateEffect(_isActive));
        _isActive = true;
    }

    #region "Actions"

    protected abstract void EnactSummerActions(bool initialAction);
    protected abstract void EnactWinterActions(bool initialAction);
    protected abstract void EnactAutumnActions(bool initialAction);
    protected abstract void EnactSpringActions(bool initialAction);

    #endregion

    private IEnumerator AnimateEffect(bool otherEffect)
    {
        GameObject activeEffect = null;

        if(!otherEffect)
        {
            DelegatesAndEvents.ElementActivated();

            if(_interactionSound != null)
                _as.PlayOneShot(_interactionSound);

            if(_interactionEffect != null)
                activeEffect = Instantiate(_interactionEffect, transform.position, transform.rotation, transform);

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
                activeEffect = Instantiate(_otherInteractionEffect, transform.position, transform.rotation, transform);
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

    private void OnDisable()
    {
        ResetElement();
    }
}