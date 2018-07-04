using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersOpen : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Alpha5)) {
			FlowerOpen ();
		}
	}

	void FlowerOpen () {
 		StartCoroutine (ScaleUpOverTime(3));
	}

	IEnumerator ScaleUpOverTime (float time) {
		Vector3 originalScale = transform.localScale;
		Vector3 destinationScale = new Vector3(1.0f, 1.0f, 1.0f);

		float currentTime = 0.0f;

		do {
			transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);
	}
}
