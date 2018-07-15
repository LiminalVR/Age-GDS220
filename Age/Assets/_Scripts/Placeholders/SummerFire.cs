using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerFire : MonoBehaviour {

	ParticleSystem pT;

	void Start () {
		pT = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Alpha3)) {
			//Fire //Need sun effects

		}
	}
}
