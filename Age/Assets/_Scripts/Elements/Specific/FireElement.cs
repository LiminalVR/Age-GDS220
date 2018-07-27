using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _sunrayPT;
	[SerializeField] private ParticleSystem _cloudsPT;
	[SerializeField] private Light _sunLight;
    [SerializeField] private Color _cloudStartColour;
	[SerializeField] private float _minSunIntensity, _maxSunIntensity, _cloudPartEffectDuration, _shineDuration, _sunReturnDuration;
	private ParticleSystem.ColorOverLifetimeModule cloudModule;
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
			//Pulse of light PT
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
            StartCoroutine(FirePulseEffects(4f));
        }
        else
        {
            _smokePT.Play();
        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

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
        cloudModule = _cloudsPT.colorOverLifetime;

        do
        {
            currentTime += Time.deltaTime / duration;

            if(fadeCloudsOut)
                cloudModule.color = Color.Lerp(_cloudStartColour, Color.clear, currentTime);
            else
                cloudModule.color = Color.Lerp(Color.clear, _cloudStartColour, currentTime);

           		_sunLight.intensity = Mathf.Lerp(startIntensity, targetSunIntensity, currentTime);

        		yield return null;
        }
        while(currentTime < duration);
    }

    private IEnumerator FirePulseEffects(float time)
    {

        float currentTime = 0.0f;

        fireShapeModule.radius = 0.65f;
        fireEmissionModule.rateOverTime = 35f;

        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        fireShapeModule.radius = 0.5f;
        fireEmissionModule.rateOverTime = 20f;
    }
}