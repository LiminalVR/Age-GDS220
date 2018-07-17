using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElement : MonoBehaviour, IElement {

    [Header("Interaction")]
    [SerializeField] private float _confirmationDuration;
    [SerializeField] private GameObject _confirmingEffect;
    [SerializeField] protected AudioClip _interactionSound;
    [SerializeField] protected GameObject _interactionEffect;
    [SerializeField] protected AudioClip _otherInteractionSound;
    [SerializeField] protected GameObject _otherInteractionEffect;
    private Color[] _startColours;

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

            Debug.Log(_confirmationTime);
        }
    }

    private void Awake()
    {
        _startColours = GetColours(_renderersToFade);
    }

    public void ResetElement()
    {
        if(gameObject.activeSelf)
        {
            _isActive = false;
            _isConfirming = false;
            ChangeColours(_renderersToFade, _startColours);

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

            Debug.Log(activeEffect);

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

            if(_otherInteractionEffect != null)
                activeEffect = Instantiate(_otherInteractionEffect, transform.position, transform.rotation, transform);
        }

        yield return null;
    }

    private IEnumerator Fade(Renderer[] renderers, float alpha, float duration)
    {
        Color[] startColours = GetColours(_renderersToFade);

        var step = 0.0f;

        while(step < 1)
        {
            step += Time.deltaTime / duration;

            for(int index = 0; index < startColours.Length; index++)
            {
                Material mat = renderers[index].material;
                Color startColour = startColours[index];
                Color targetColour = ChangeAlpha(mat.color, alpha);

                mat.color = Color.Lerp(startColour, targetColour, step);
                renderers[index].material = mat;
            }

            yield return null;
        }

        yield return null;
    }

    private void ChangeColours(Material[] materials, Color[] targetColours)
    {
        for(int index = 0; index < materials.Length; index++)
        {
            materials[index].color = targetColours[index];
        }
    }

    private void ChangeColours(Renderer[] renderers, Color[] targetColours)
    {
        for(int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = targetColours[index];
        }
    }

    private Color[] GetColours(Renderer[] renderers)
    {
        List<Color> colours = new List<Color>();

        foreach(Renderer rend in renderers)
        {
            colours.Add(rend.material.color);
        }

        return colours.ToArray();
    }

    private Color[] GetColours(Material[] materials)
    {
        List<Color> colours = new List<Color>();

        foreach(Material mat in materials)
        {
            colours.Add(mat.color);
        }

        return colours.ToArray();
    }

    private Color ChangeAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}