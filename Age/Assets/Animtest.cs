using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour {

	public Animator anim;
	public GameObject blueFlowerPetal, blueFlowerBud;

	// Use this for initialization
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
			WaterOrbSummer ();
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
		
	}

	void WaterOrbSummer () {
		blueFlowerPetal.gameObject.SetActive (true);
		StartCoroutine (ScaleUpOverTime(3));
	}

	IEnumerator ScaleUpOverTime (float time) {
		Vector3 originalScale = blueFlowerPetal.transform.localScale;
		Vector3 destinationScale = new Vector3(1.0f, 1.0f, 1.0f);
		Vector3 budOriginalScale = blueFlowerBud.transform.localScale;
		Vector3 budDestinationScale = new Vector3(0.0f, 1.0f, 0.0f);

		float currentTime = 0.0f;

		do {
			blueFlowerPetal.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			blueFlowerBud.transform.localScale = Vector3.Lerp(budOriginalScale, budDestinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
			blueFlowerBud.gameObject.SetActive(false);
		} 
		while (currentTime <= time);
	}
}