using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : BaseElement {

	#region Summer
	[SerializeField] ParticleSystem _sunrayPT;
	[SerializeField] ParticleSystem _cloudsPT;
	[SerializeField] Light _light;
	[SerializeField] Color _cloudStartColour;
	ParticleSystem.ColorOverLifetimeModule cloudModule;
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
			StartCoroutine (CloudFade(6f));
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

	private IEnumerator CloudFade(float duration)
	{
		float currentTime = 0.0f;
		cloudModule = _cloudsPT.colorOverLifetime;

		var minIntensity = 1f;
		var maxIntensity = minIntensity + 0.6f;

		do
		{
			cloudModule.color = Color.Lerp(_cloudStartColour, Color.clear, currentTime);

			_light.intensity = _light.intensity < maxIntensity ? _light.intensity + currentTime : maxIntensity;

			currentTime += Time.deltaTime/duration;
			yield return null;
		}
		while(currentTime <= duration);
	}
}