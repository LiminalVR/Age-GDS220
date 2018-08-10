using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _airGustPT;
    private GameObject[] _stemBase;
    [SerializeField] private float _wiggleDuration;
    [SerializeField] private float _wiggleAngle;
    #endregion

    #region Autumn
    [Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _auLeafTuftPT;
    ParticleSystem.NoiseModule fireNoiseModule;
	[SerializeField] private GameObject _rainPT;
	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private ParticleSystem _emberBurstPT;
    #endregion

    #region Spring
    //[Header("Spring")]
    private List<ParticleSystem> _dandelionStillPT;
    private List<ParticleSystem> _dandelionBlowPT;
    #endregion

    private void Start ()
    {
        _dandelionStillPT = new List<ParticleSystem>();
        _dandelionBlowPT = new List<ParticleSystem>();

        //Particle declaration for enabling noise on campfire
        fireNoiseModule = _firePT.noise;

        _stemBase = GameObject.FindGameObjectsWithTag("StemBase");

        var _findParticles = FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem p in _findParticles)
        {
            switch (p.tag)
            {
                case ("DandelionStillParticle"):
                    _dandelionStillPT.Add(p);
                    break;
                case ("DandelionBlowParticle"):
                    _dandelionBlowPT.Add(p);
                    break;
                default:
                    break;
            }
        }
    }

    //Air gust particles and terrain grass wind strength
    protected override void EnactSummerActions()
    {
        StartCoroutine(WindGust(5.4f));
    }

    //Air and leaves particles, tilt rain particles if applicable
    protected override void EnactAutumnActions()
    {
        StartCoroutine(WindGust(5.4f));

        _auLeafTuftPT.Play();

        StartCoroutine(AirRainingEffects(5.4f));
    }

    //Air gust, fire ember, tilt rain / fire if applicable
    protected override void EnactWinterActions()
    {
        StartCoroutine(WindGust(5.4f));

        _emberBurstPT.Play();

        StartCoroutine(AirRainingEffects(3.8f));
    }

    //Blows dandelion pollen in wind
    protected override void EnactSpringActions()
    {
        //Blow pollen off dandelions
        foreach (ParticleSystem p in _dandelionStillPT)
        {
            p.Stop();
        }
        foreach (ParticleSystem p in _dandelionBlowPT)
        {
            p.Play();
        }
    }

	private IEnumerator WindGust (float delay) {

        _airGustPT.Play();

        yield return new WaitForSeconds(delay);

        Wiggle(_stemBase);

        yield return null;
    }

    private void Wiggle(GameObject[] _objectArray)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(RotateTo(g, _wiggleDuration));
        }
    }

    private IEnumerator RotateTo(GameObject g, float time)
    {
        {
            float currentTime = 0.0f;

            {
                do
                {
                    g.transform.localEulerAngles = new Vector3(g.transform.rotation.eulerAngles.x, g.transform.rotation.eulerAngles.y, Mathf.PingPong(currentTime * 5f, _wiggleAngle));

                    currentTime += Time.deltaTime;
                    yield return null;
                }
                while (currentTime <= time);
            }
        }
    }

    private IEnumerator AirRainingEffects (float time) {

		float currentTime = 0.0f;

		_rainPT.transform.rotation = Quaternion.Euler (-115f, -45f, 90f);

		yield return new WaitForSeconds(5f);

		fireNoiseModule.enabled = true;

		do {
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);
	
		_rainPT.transform.rotation = Quaternion.Euler (-90f, -45f, 90f);
		fireNoiseModule.enabled = false;
	}
}