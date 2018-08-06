using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

	Terrain _Terrain;

	#region Summer
	[Header("Summer")]
	[SerializeField] private ParticleSystem _airGustPT;
	[SerializeField] private ParticleSystem _airGustWeakerPT;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private ParticleSystem _firePT;
    [SerializeField] private ParticleSystem _leavesWeakPT;
    ParticleSystem.NoiseModule fireNoiseModule;
	[SerializeField] private GameObject _rainPT;
	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private ParticleSystem _emberPT;
    #endregion

    #region Spring
    [Header("Spring")]
    [SerializeField] private ParticleSystem _dandelionWindPT;
    [HideInInspector] public List<ParticleSystem> _dandelionStillPT = new List<ParticleSystem>();
    private List<ParticleSystem> _dandelionBlowPT = new List<ParticleSystem>();
    #endregion

    private void Start () {
		//Particle declaration for enabling noise on campfire
		fireNoiseModule = _firePT.noise;

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

    //1: Air gust particles and terrain grass wind strength 2: Repeat with lessened effect
    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustPT.Play ();
			StartCoroutine (SummerGust(5.4f));
        }
        else
        {
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustWeakerPT.Play ();
			StartCoroutine (SummerGust(4.4f));
        }
    }

    //1: Air and leaves particles, tilt rain particles if applicable 2: Repeat with lessened effect, tilt rain particles if applicable
    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {
			_airGustPT.Play ();
			_leavesWeakPT.Play ();

			StartCoroutine (AirRainingEffects(5.4f));
        }
        else
        {
			_airGustWeakerPT.Play ();
			_leavesWeakPT.Play ();

			StartCoroutine (AirRainingEffects(4.4f));
        }
    }

    //1: Air gust, tilt rain / fire if applicable 2: Fire ember particle burst
    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {
			_airGustPT.Play ();
			StartCoroutine (AirRainingEffects(3.8f));
        }
        else
        {
			_emberPT.Play ();
			StartCoroutine (AirRainingEffects(3f));
        }
    }

    //1: Blows dandelion pollen in wind 2: Carries dandelion pollen around scene
    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
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
        else
        {
            _dandelionWindPT.Play();
        }
    }

	private IEnumerator SummerGust (float time) {

		float currentTime = 0.0f;

		yield return new WaitForSeconds(1.5f);

		do {
			_Terrain.terrainData.wavingGrassStrength = 1f;
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);
		//Resets wind power back to 0.3 after gusting complete
		_Terrain.terrainData.wavingGrassStrength = 0.3f;
	}

	private IEnumerator AirRainingEffects (float time) {

		float currentTime = 0.0f;

		_rainPT.transform.rotation = Quaternion.Euler (-115f, -45f, 90f);

		yield return new WaitForSeconds(1.5f);

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