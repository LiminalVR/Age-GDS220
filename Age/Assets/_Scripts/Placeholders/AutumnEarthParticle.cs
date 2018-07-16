using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutumnEarthParticle : MonoBehaviour {

	ParticleSystem petalPT;

	void Start () {
		petalPT = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Alpha5)) {
			//Earth
			petalPT.Play();
		}
	}
}