using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _sunrayPT;
    [SerializeField] private AudioClip _sunrayAC;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _minSunIntensity = 0f, _maxSunIntensity = 0f, _cloudFadeOutDuration = 0f, _shineDuration = 0f, _cloudFadeInDuration = 0f;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _kindlePT;
    [SerializeField] private AudioSource _asFire;
    [SerializeField] private AudioClip _fireKindleAC;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] private ParticleSystem _firePulsePT;
	#endregion

	#region Spring

	#endregion

    //Fade clouds and show sun ray particles
    protected override void EnactSummerActions()
    {
        _sunrayPT.Play();
        StartCoroutine(AudioDelay(8f, _sunrayAC));
        StartCoroutine(Shine());
    }

    //Kindle fire alight
    protected override void EnactAutumnActions()
    {
        _kindlePT.Play();
        _as.PlayOneShot(_fireKindleAC);
        _firePT.Play();
        _asFire.Play();
    }

    //Fire burns brighter
    protected override void EnactWinterActions()
    {
        StartCoroutine(FirePulseEffect(6f));
    }

    //Sun rays, see summer
    protected override void EnactSpringActions()
    {
        _sunrayPT.Play();
        StartCoroutine(AudioDelay(8f, _sunrayAC));
        StartCoroutine(Shine());
    }

    private IEnumerator AudioDelay(float _delay, AudioClip _ac)
    {
        _activeCoroutines++;
        yield return new WaitForSeconds(_delay);

        _as.PlayOneShot(_ac);

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }

    private IEnumerator Shine()
    {
        _activeCoroutines++;
        StartCoroutine(ManipulateShine(_maxSunIntensity, _cloudFadeOutDuration, true));

        yield return new WaitForSeconds (_cloudFadeOutDuration + _shineDuration);

        StartCoroutine(ManipulateShine(_minSunIntensity, _cloudFadeInDuration, false));

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }

    private IEnumerator ManipulateShine(float targetSunIntensity, float _duration, bool _fading)
    {
        _activeCoroutines++;
        float currentTime = 0.0f;

        float startIntensity = _sunLight.intensity;

        do
        {
            currentTime += Time.deltaTime;

            _sunLight.intensity = Mathf.Lerp(startIntensity, targetSunIntensity, currentTime);

            yield return null;
        }
        while(currentTime < _duration);

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }

    private IEnumerator FirePulseEffect(float _duration)
    {
        _activeCoroutines++;

        _firePT.Stop();
        _asFire.Stop();
        _firePulsePT.Play();

        yield return new WaitForSeconds(_duration);

        _firePT.Play();
        _asFire.Play();

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }
}