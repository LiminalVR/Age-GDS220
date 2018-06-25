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
		if (Input.GetKey(KeyCode.Alpha1)) {

			//Use this to stomp out the campfire
			anim.SetBool ("cFireAlive", false);
			anim.SetBool ("cFireDie", true);
		}

			//Use this to repair the campfire
		if (Input.GetKey(KeyCode.Alpha2)) {
			anim.SetBool ("cFireDie", false);
			anim.SetBool ("cFireAlive", true);
		}
	}
}
