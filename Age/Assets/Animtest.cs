using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

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


}