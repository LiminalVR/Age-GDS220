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
    private Color[] _startColours;
    private ColourMaster _colourMaster;

    [Header("Fade")]
    [SerializeField] private Renderer[] _renderersToFade;
    [SerializeField] private ParticleSystem[] _particlesSystemsToFade;
    [SerializeField] private float _maxAlpha;
    [SerializeField] private float _fadeDuration;

    public ElementManager _elementManager;

    protected AudioSource _as;
    protected bool _isActive = false;
    protected bool _isConfirming = false;
    private float _confirmationTime;

    public float ConfirmationTime
    {
        get { return _confirmationTime; }
        set
        {
            if(_isActive == false)
            {
                
                _isConfirming = false;

                if(_confirmingEffect.activeSelf)
                    _confirmingEffect.SetActive(false);
            }
            else
            {
                _isConfirming = true;

                if(!_confirmingEffect.activeSelf)
                    _confirmingEffect.SetActive(true);
            }

            _confirmationTime = value;

            if(_confirmationTime > _confirmationDuration)
            {
                    _confirmationTime = 0;
                    _isConfirming = false;
                    Interact();
            }
        }
    }

    // Temp.
    private void Setup()
    {
        _elementManager = FindObjectOfType<ElementManager>();
        _as = GetComponent<AudioSource>();
        _colourMaster = new ColourMaster();
        _startColours = _colourMaster.GetColours(_renderersToFade);
    }

    public void Init()
    {
        StartCoroutine(Fade(_renderersToFade, _maxAlpha, _fadeDuration));
    }

    public void ResetElement()
    {
        // Temp.
        Setup();

        if(gameObject.activeSelf)
        {
            _isActive = false;
            _isConfirming = false;

            ResetColours();

            gameObject.SetActive(false);
        }
    }

    private void ResetColours()
    {
        for(int index = 0; index < _startColours.Length; index++)
        {
            _startColours[index] = _colourMaster.ChangeAlpha(_startColours[index], 0.0f);
        }

        _colourMaster.ChangeColours(_renderersToFade, _startColours);
    }

    public virtual void Interact()
    {
        switch(SeasonManager._currentSeasonType)
        {
            case SeasonManager.SeasonType.SUMMER:
                EnactSummerActions();
                break;

            case SeasonManager.SeasonType.AUTUMN:
                EnactAutumnActions();
                break;

            case SeasonManager.SeasonType.WINTER:
                EnactWinterActions();
                break;

            case SeasonManager.SeasonType.SPRING:
                EnactSpringActions();
                break;

            default:
                Debug.LogError("Unknown Season");
                break;
        }

        StartCoroutine(AnimateEffect());
        _isActive = true;

        StartCoroutine(DeactivateElement());
    }

    #region "Actions"

    protected abstract void EnactSummerActions();
    protected abstract void EnactWinterActions();
    protected abstract void EnactAutumnActions();
    protected abstract void EnactSpringActions();

    #endregion

    private IEnumerator AnimateEffect()
    {
        GameObject activeEffect = null;

        DelegatesAndEvents.ElementActivated();

        if(_interactionSound != null)
            _as.PlayOneShot(_interactionSound);

        if(_interactionEffect != null)
            activeEffect = Instantiate(_interactionEffect, transform.position, transform.rotation, transform);

        while(activeEffect != null)
        {
            yield return null;
        }

        yield return null;
    }

    private IEnumerator DeactivateElement()
    {
        StartCoroutine(Fade(_renderersToFade, 0.0f, _fadeDuration));

        yield return new WaitForSeconds(_fadeDuration);

        gameObject.SetActive(false);

        yield return null;
    }

    private IEnumerator Fade(Renderer[] renderers, float targetAlpha, float duration)
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
                Color targetColour = _colourMaster.ChangeAlpha(mat.color, targetAlpha);

                mat.color = Color.Lerp(startColour, targetColour, step);
                renderers[index].material = mat;
            }

            for(int index = 0; index < _particlesSystemsToFade.Length; index++)
            {
                Color startColour = _particlesSystemsToFade[index].main.startColor.color;
                Color targetColour = _colourMaster.ChangeAlpha(startColour, targetAlpha);
                ParticleSystem.ColorOverLifetimeModule colourModule = _particlesSystemsToFade[index].colorOverLifetime;


                colourModule.color = Color.Lerp(startColour, targetColour, step);

            }

            yield return null;
        }

        yield return null;
    }
}