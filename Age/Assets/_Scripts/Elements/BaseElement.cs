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
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Gradient _particleFadeGrad;

    private List<ParticleSystem.ColorOverLifetimeModule> _colourModules;

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
                StartCoroutine(Deactive(_renderersToFade, _fadeDuration));
                _confirmationTime = 0;
                _isConfirming = false;
                Interact();
            }
        }
    }

    // Temp.
    private void Setup()
    {
        _as = GetComponent<AudioSource>();
        _colourMaster = new ColourMaster();
        _startColours = _colourMaster.GetColours(_renderersToFade);


        //_colourModules = new List<ParticleSystem.ColorOverLifetimeModule>();
        //foreach(ParticleSystem partSys in _particlesSystemsToFade)
        //{
        //    ParticleSystem.ColorOverLifetimeModule colourModule = partSys.GetComponent<ParticleSystem.ColorOverLifetimeModule>();
        //    colourModule = partSys.colorOverLifetime;
        //    _colourModules.Add(colourModule);
        //}
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

        StartCoroutine(AnimateEffect());
        _isActive = true;
    }

    #region "Actions"

    protected abstract void EnactSummerActions(bool initialAction);
    protected abstract void EnactWinterActions(bool initialAction);
    protected abstract void EnactAutumnActions(bool initialAction);
    protected abstract void EnactSpringActions(bool initialAction);

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

    
    private IEnumerator Deactive(Renderer[] renderers, float duration)
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
                Color targetColour = _colourMaster.ChangeAlpha(mat.color, 0.0f);

                mat.color = Color.Lerp(startColour, targetColour, step);
                renderers[index].material = mat;
            }

            for(int index = 0; index < _particlesSystemsToFade.Length; index++)
            {
                Color startColour = _particlesSystemsToFade[index].main.startColor.color;
                ParticleSystem.ColorOverLifetimeModule colourModule = _particlesSystemsToFade[index].colorOverLifetime;


                colourModule.color = Color.Lerp(startColour, Color.clear, step);

            }

            yield return null;
        }

        gameObject.SetActive(false);

        yield return null;
    }
}