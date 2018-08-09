using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseElement : MonoBehaviour, IElement {

    [Header("Interaction")]
    [SerializeField] private float _confirmationDuration;
    [SerializeField] private GameObject _confirmingEffect;
    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    [SerializeField] protected AudioClip _otherInteractionSound;
    private Color[] _startColours;
    private ColourMaster _colourMaster;

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
            {
                _isConfirming = false;

                if(_confirmingEffect.activeSelf)
                {
                    _confirmingEffect.SetActive(false);
                }
            }
            else
            {
                _isConfirming = true;

                if(!_confirmingEffect.activeSelf)
                {
                    _confirmingEffect.SetActive(true);
                }
            }

            _confirmationTime = value;

            if(_confirmationTime > _confirmationDuration)
            {
                _confirmationTime = 0;
                _isConfirming = false;
                Interact();
            }

            //Debug.Log(_confirmationTime);
        }
    }

    private void Awake()
    {
    }

    // Temp.
    private void Setup()
    {
        _as = GetComponent<AudioSource>();
        _colourMaster = new ColourMaster();
        _startColours = _colourMaster.GetColours(_renderersToFade);
    }

    public void ResetElement()
    {
        // Temp.
        Setup();

        if(gameObject.activeSelf)
        {
            _isActive = false;
            _isConfirming = false;
            _colourMaster.ChangeColours(_renderersToFade, _startColours);

            gameObject.SetActive(false);
        }
    }

    public virtual void Interact()
    {
        switch(SeasonManager._currentSeasonType)
        {
            case SeasonManager.SeasonType.SUMMER:
                EnactSummerActions(!_isActive);
                break;

            case SeasonManager.SeasonType.AUTUMN:
                EnactAutumnActions(!_isActive);
                break;

            case SeasonManager.SeasonType.WINTER:
                EnactWinterActions(!_isActive);
                break;

            case SeasonManager.SeasonType.SPRING:
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

            while(activeEffect != null)
            {
                yield return null;
            }

            StartCoroutine(Fade(_renderersToFade, _targetAlpha, _fadeDuration));
        }
        else
        {
            if(_otherInteractionSound != null)
                _as.PlayOneShot(_otherInteractionSound);
        }

        yield return null;
    }

    private IEnumerator Fade(Renderer[] renderers, float alpha, float duration)
    {
        Color[] startColours = _colourMaster.GetColours(_renderersToFade);

        var step = 0.0f;

        while(step < 1)
        {
            step += Time.deltaTime / duration;

            for(int index = 0; index < startColours.Length; index++)
            {
                Material mat = renderers[index].material;
                Color startColour = startColours[index];
                Color targetColour = _colourMaster.ChangeAlpha(mat.color, alpha);

                mat.color = Color.Lerp(startColour, targetColour, step);
                renderers[index].material = mat;
            }

            yield return null;
        }

        yield return null;
    }
}