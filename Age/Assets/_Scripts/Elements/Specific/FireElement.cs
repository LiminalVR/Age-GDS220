using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _sunrayPT;
    [SerializeField] private ParticleSystem _sunrayPulsePT;
    [SerializeField] private ParticleSystem _cloudsPT;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _minSunIntensity, _maxSunIntensity, _cloudPartEffectDuration, _shineDuration, _sunReturnDuration;
    [SerializeField] Gradient _cloudsGradient;
    private GradientAlphaKey[] _cGAK;
    private GradientColorKey[] _cGCK;
	private ParticleSystem.ColorOverLifetimeModule _cloudModule;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _kindlePT;
    [SerializeField] private ParticleSystem _emberPT;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] private ParticleSystem _smokePT;
    private ParticleSystem.EmissionModule fireEmissionModule;
	private ParticleSystem.ShapeModule fireShapeModule;
    [SerializeField] private float _minRadius, _maxRadius, _minRate, _maxRate;
	#endregion

	#region Spring

	#endregion

	/*
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {

			EnactSummerActions (true);
		}
	}
	*/


	private void Start () 
	{
        _cloudModule = _cloudsPT.colorOverLifetime;
        _cloudsGradient = new Gradient();

        _cGAK = new GradientAlphaKey[4];
        _cGAK[0].alpha = 0.0f;
        _cGAK[0].time = 0.0f;
        _cGAK[1].alpha = 0.7f;
        _cGAK[1].time = 0.1f;
        _cGAK[2].alpha = 0.7f;
        _cGAK[2].time = 0.9f;
        _cGAK[3].alpha = 0.0f;
        _cGAK[3].time = 1.0f;

        _cGCK = new GradientColorKey[2];
        _cGCK[0].color = Color.white;
        _cGCK[0].time = 0.0f;
        _cGCK[1].color = Color.white;
        _cGCK[1].time = 1.0f;

        _cloudsGradient.SetKeys(_cGCK, _cGAK);

        _cloudModule.color = _cloudsGradient;

        fireShapeModule = _firePT.shape;
		fireEmissionModule = _firePT.emission;
	}

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
			_sunrayPT.Play ();
			StartCoroutine (Shine());
		}
        else
        {
            _sunrayPulsePT.Play();
			StartCoroutine (Shine());
        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {
			_kindlePT.Play ();
			_firePT.Play ();
        }
        else
        {
			_emberPT.Play ();
        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {
            StartCoroutine(FirePulseEffects(4f, _minRadius, _maxRadius, _minRate, _maxRate));
        }
        else
        {
            _smokePT.Play();
        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if (initialAction)
        {
            _sunrayPT.Play();
            StartCoroutine(Shine());
        }
        else
        {
            _sunrayPulsePT.Play();
            StartCoroutine(Shine());
        }
    }

    private IEnumerator Shine()
    {
        StartCoroutine(ManipulateShine(true, _maxSunIntensity, _cloudPartEffectDuration));

        yield return new WaitForSeconds(_cloudPartEffectDuration + _shineDuration);

        StartCoroutine(ManipulateShine(false, _minSunIntensity, _sunReturnDuration));

        yield return null;
    }

    private IEnumerator ManipulateShine(bool fadeCloudsOut, float targetSunIntensity, float duration)
    {
        float currentTime = 0.0f;

        float startIntensity = _sunLight.intensity;

        do
        {
            currentTime += Time.deltaTime / duration;

            if (fadeCloudsOut == true)
            {
                _cloudModule.color = _cloudsGradient;

                _cGCK[0].color = Color.Lerp(Color.white, Color.clear, currentTime);
                _cGCK[1].color = Color.Lerp(Color.white, Color.clear, currentTime);
                _cloudsGradient.SetKeys(_cGCK, _cGAK);
            }
            else
            {
                _cloudModule.color = _cloudsGradient;

                _cGCK[0].color = Color.Lerp(Color.white, Color.white, currentTime);
                _cGCK[1].color = Color.Lerp(Color.white, Color.white, currentTime);
                _cloudsGradient.SetKeys(_cGCK, _cGAK);
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