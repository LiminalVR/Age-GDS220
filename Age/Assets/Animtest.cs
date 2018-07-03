using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour {

	public Animator anim;
	public GameObject firePT, rainPT;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			EarthOrbSpring ();
		}
		if (Input.GetKey(KeyCode.Alpha2)) {
			EarthOrbSummer ();
		}
		if (Input.GetKey(KeyCode.Alpha3)) {
			FireOrbAutumn ();
		}
		if (Input.GetKey(KeyCode.Alpha4)) {
			MakeRain ();
		}
	}

	//Campfire collapser
	void EarthOrbSpring () {
		anim.SetBool ("cFireAlive", false);
		anim.SetBool ("cFireDie", true);
	}

	//Campfire prepare / repair
	void EarthOrbSummer () {
		anim.SetBool ("cFireDie", false);
		anim.SetBool ("cFireAlive", true);
	}

	//Light up campfire
	void FireOrbAutumn () {
		firePT.gameObject.SetActive (true);
	}

	//Make rain
	void MakeRain() {
		rainPT.gameObject.SetActive (true);
	}

}