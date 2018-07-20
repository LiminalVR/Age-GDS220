using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirElement : BaseElement {

	Terrain _Terrain;

	#region Summer
	[SerializeField] private ParticleSystem _airGustPT;
	#endregion

	#region Autumn

	#endregion

	#region Winter

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
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustPT.Play ();
			StartCoroutine (SummerGust(4.6f));
        }
        else
        {
			_Terrain = FindObjectOfType<Terrain> ();
			_airGustPT.Play ();
			StartCoroutine (SummerGust(4.6f));
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
}