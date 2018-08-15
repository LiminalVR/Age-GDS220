using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _airGustPT;
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    [SerializeField] private float _angle;
    #endregion

    #region Autumn
    [Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _auLeafTuftPT;
    private ParticleSystem.NoiseModule fireNoiseModule;
	[SerializeField] private GameObject _rainPT;
	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private ParticleSystem _emberBurstPT;
    #endregion

    #region Spring

    #endregion

    private void Start ()
    {
        //Particle declaration for enabling noise on campfire
        fireNoiseModule = _firePT.noise;
    }

    //Air gust particles and terrain grass wind strength
    protected override void EnactSummerActions()
    {
        StartCoroutine(WindGust(_delay));
    }

    //Air and leaves particles, tilt rain particles if applicable
    protected override void EnactAutumnActions()
    {
        StartCoroutine(WindGust(_delay));

        _auLeafTuftPT.Play();

        StartCoroutine(AirRainingEffects(_duration));
    }

    //Air gust, fire ember, tilt rain / fire if applicable
    protected override void EnactWinterActions()
    {
        StartCoroutine(WindGust(_delay));

        _emberBurstPT.Play();

        StartCoroutine(AirRainingEffects(_duration));
    }

    //Blows dandelion pollen in wind
    protected override void EnactSpringActions()
    {
        //Blow pollen off dandelions
        foreach (ParticleSystem p in _elementManager._dandelionStillPT)
        {
            p.Stop();
        }
        foreach (ParticleSystem p in _elementManager._dandelionBlowPT)
        {
            p.Play();
        }
    }

	private IEnumerator WindGust (float _delay) {

        _airGustPT.Play();

        yield return new WaitForSeconds(_delay);

        _elementManager.Wiggle(_elementManager._stemBase, _duration, _angle);

        yield return null;
    }

    private IEnumerator AirRainingEffects (float _duration) {

		float currentTime = 0.0f;

		_rainPT.transform.rotation = Quaternion.Euler (-115f, -45f, 90f);

		yield return new WaitForSeconds(5f);

		fireNoiseModule.enabled = true;
        _elementManager._smokeTrailPT.Play();

        do {
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= _duration);
	
		_rainPT.transform.rotation = Quaternion.Euler (-90f, -45f, 90f);
		fireNoiseModule.enabled = false;
	}
}