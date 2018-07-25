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
	[SerializeField] private ParticleSystem _leavesPT, _leavesWeakPT;
	ParticleSystem.NoiseModule fireNoiseModule;
	[SerializeField] private GameObject _rainPT;
	#endregion

	#region Winter

	#endregion

	#region Spring

	#endregion

	/*
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {

			EnactAutumnActions (true);
		}
	}
	*/

	private void Start () {
		//Particle declaration for enabling noise on campfire
		fireNoiseModule = _firePT.noise;
	}

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustPT.Play ();
			StartCoroutine (SummerGust(4.6f));
        }
        else
        {
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustWeakerPT.Play ();
			StartCoroutine (SummerGust(3.6f));
        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {
			_airGustPT.Play ();
			_leavesPT.Play ();

			StartCoroutine (AirRainingEffects(4.6f));
		
        }
        else
        {
			_airGustWeakerPT.Play ();
			_leavesWeakPT.Play ();

			StartCoroutine (AirRainingEffects(3.6f));
        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {
			_airGustPT.Play ();
			StartCoroutine (AirRainingEffects(3.8f));
        }
        else
        {
			//Burst of ember particles
			StartCoroutine (AirRainingEffects(3f));
        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {
			//Blow pollen off dandelions
        }
        else
        {
			//Wind carrying pollen misc PT
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