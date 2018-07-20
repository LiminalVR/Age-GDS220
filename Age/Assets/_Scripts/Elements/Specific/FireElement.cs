using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[SerializeField] private ParticleSystem _sunrayPT;
	[SerializeField] private ParticleSystem _cloudsPT;
	[SerializeField] private Light _light;
    [SerializeField] private Color _cloudStartColour;
    [SerializeField] private float _minSunIntensity;
    [SerializeField] private float _maxSunIntensity;
    [SerializeField] private float _cloudPartEffectDuration;
    [SerializeField] private float _shineDuration;
    [SerializeField] private float _sunReturnDuration;
	private ParticleSystem.ColorOverLifetimeModule cloudModule;
	#endregion

	#region Autumn

	#endregion

	#region Winter

	#endregion

	#region Spring

	#endregion

	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {

			EnactSummerActions (true);
		}
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
        float startIntensity = _light.intensity;
        cloudModule = _cloudsPT.colorOverLifetime;

        do
        {
            currentTime += Time.deltaTime / duration;

            if(fadeCloudsOut)
                cloudModule.color = Color.Lerp(_cloudStartColour, Color.clear, currentTime);
            else
                cloudModule.color = Color.Lerp(Color.clear, _cloudStartColour, currentTime);

            _light.intensity = Mathf.Lerp(startIntensity, targetSunIntensity, currentTime);
            yield return null;
        }
        while(currentTime < duration);
    }
}