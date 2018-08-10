using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _sunrayPT;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _minSunIntensity = 0f, _maxSunIntensity = 0f, _cloudFadeOutDuration = 0f, _shineDuration = 0f, _cloudFadeInDuration = 0f;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _kindlePT;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] private ParticleSystem _smokeTrailPT;
    private ParticleSystem.EmissionModule fireEmissionModule;
	private ParticleSystem.ShapeModule fireShapeModule;
    [SerializeField] private float _minRadius = 0f, _maxRadius = 0f, _minRate = 0f, _maxRate = 0f;
	#endregion

	#region Spring

	#endregion

	private void Start () 
	{
        fireShapeModule = _firePT.shape;
		fireEmissionModule = _firePT.emission;
	}

    //Fade clouds and show sun ray particles
    protected override void EnactSummerActions()
    {
        _sunrayPT.Play();
        StartCoroutine(Shine());
    }

    //Kindle fire alight
    protected override void EnactAutumnActions()
    {
        _kindlePT.Play();
        _firePT.Play();
    }

    //Fire burns brighter
    protected override void EnactWinterActions()
    {
        StartCoroutine(FirePulseEffects(5f, _minRadius, _maxRadius, _minRate, _maxRate));

        _smokeTrailPT.Play();
    }

    //Sun rays, see summer
    protected override void EnactSpringActions()
    {
        _sunrayPT.Play();
        StartCoroutine(Shine());
    }

    private IEnumerator Shine()
    {
        StartCoroutine(ManipulateShine(_maxSunIntensity, _cloudFadeOutDuration, true));

        yield return new WaitForSeconds (_cloudFadeOutDuration + _shineDuration);

        StartCoroutine(ManipulateShine(_minSunIntensity, _cloudFadeInDuration, false));

        yield return null;
    }

    private IEnumerator ManipulateShine(float targetSunIntensity, float _duration, bool _fading)
    {
        float currentTime = 0.0f;

        float startIntensity = _sunLight.intensity;

        do
        {
            currentTime += Time.deltaTime;

            /*
            if (fading)
            {
                cloudsMainModule.startColor = Color.Lerp(cloudOpaque, Color.clear, currentTime);
            }
            else
            {
                cloudsMainModule.startColor = Color.Lerp(Color.clear, cloudOpaque, currentTime);
            }
            */

            _sunLight.intensity = Mathf.Lerp(startIntensity, targetSunIntensity, currentTime);

            yield return null;
        }
        while(currentTime < _duration);
    }

    private IEnumerator FirePulseEffects(float time, float _minRadius, float _maxRadius, float _minRate, float _maxRate)
    {

        float currentTime = 0.0f;

        fireShapeModule.radius = _maxRadius;
        fireEmissionModule.rateOverTime = _maxRate;

        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        fireShapeModule.radius = _minRadius;
        fireEmissionModule.rateOverTime = _minRate;
    }
}