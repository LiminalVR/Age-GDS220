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

	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private ParticleSystem _campfirePT;
	private ParticleSystem.EmissionModule emissionModule;
	private ParticleSystem.ShapeModule shapeModule;
	[SerializeField] private float _minPTIntensity, _maxPTIntensity, minPTShape,maxPTShape;
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


    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
			_sunrayPT.Play ();
			StartCoroutine (Shine());
		}
        else
        {
			_sunrayPT.Play ();

        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

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
}