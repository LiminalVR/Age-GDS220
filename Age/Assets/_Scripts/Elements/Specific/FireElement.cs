using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _sunrayPT;
    public ParticleSystem _cloudsPT;
    private ParticleSystem.MainModule cloudsMainModule;
    private Color cloudOpaque = Color.white;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _minSunIntensity = 0f, _maxSunIntensity = 0f, _cloudPartEffectDuration = 0f, _shineDuration = 0f, _sunReturnDuration = 0f;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _kindlePT;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] private ParticleSystem _smokePT;
    private ParticleSystem.EmissionModule fireEmissionModule;
	private ParticleSystem.ShapeModule fireShapeModule;
    [SerializeField] private float _minRadius = 0f, _maxRadius = 0f, _minRate = 0f, _maxRate = 0f;
	#endregion

	#region Spring

	#endregion

	private void Start () 
	{
        cloudsMainModule = _cloudsPT.main;
        cloudsMainModule.startColor = cloudOpaque;

        fireShapeModule = _firePT.shape;
		fireEmissionModule = _firePT.emission;
	}

    //Fade clouds and show sun ray particles
    protected override void EnactSummerActions(bool initialAction)
    {
        _sunrayPT.Play();
        StartCoroutine(Shine());

        if (initialAction)
        {
			
		}
        else
        {
           
        }
    }

    //Kindle fire alight
    protected override void EnactAutumnActions(bool initialAction)
    {
        _kindlePT.Play();
        _firePT.Play();

        if (initialAction)
        {
			
        }
        else
        {
			
        }
    }

    //Fire burns brighter
    protected override void EnactWinterActions(bool initialAction)
    {
        StartCoroutine(FirePulseEffects(4f, _minRadius, _maxRadius, _minRate, _maxRate));

        _smokePT.Play();

        if (initialAction)
        {
            
        }
        else
        {
            
        }
    }

    //Sun rays, see summer
    protected override void EnactSpringActions(bool initialAction)
    {
        _sunrayPT.Play();
        StartCoroutine(Shine());

        if (initialAction)
        {
            
        }
        else
        {

        }
    }

    private IEnumerator Shine()
    {
        StartCoroutine(ManipulateShine(_maxSunIntensity, _cloudPartEffectDuration, true));

        yield return new WaitForSeconds (_cloudPartEffectDuration + _shineDuration);

        StartCoroutine(ManipulateShine(_minSunIntensity, _sunReturnDuration, false));

        yield return null;
    }

    private IEnumerator ManipulateShine(float targetSunIntensity, float duration, bool fading)
    {
        float currentTime = 0.0f;

        float startIntensity = _sunLight.intensity;

        do
        {
            currentTime += Time.deltaTime;

            if (fading)
            {
                cloudsMainModule.startColor = Color.Lerp(cloudOpaque, Color.clear, currentTime);
            }
            else
            {
                cloudsMainModule.startColor = Color.Lerp(Color.clear, cloudOpaque, currentTime);
            }

            _sunLight.intensity = Mathf.Lerp(startIntensity, targetSunIntensity, currentTime);

            yield return null;
        }
        while(currentTime < duration);
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