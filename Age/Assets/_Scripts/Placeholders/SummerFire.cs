using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerFire : MonoBehaviour {

	public GameObject cloudsPT;
	ParticleSystem sunraysPT;

	void Start () {
		sunraysPT = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Alpha3)) {
			//Fire
			sunraysPT.Play();
			cloudsPT.SetActive (false);
		}
	}
}