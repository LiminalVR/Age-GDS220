using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerWaterClosed : MonoBehaviour {

	void Update () {
		if (Input.GetKey(KeyCode.Alpha2)) {
			//Water
			FlowerClose ();
		}
	}

	void FlowerClose () {
		StartCoroutine (ScaleDownOverTime(3));
	}

	IEnumerator ScaleDownOverTime (float time) {
		Vector3 budOriginalScale = transform.localScale;
		Vector3 budDestinationScale = new Vector3(0.0f, 1.0f, 0.0f);

		float currentTime = 0.0f;

		do {
			transform.localScale = Vector3.Lerp(budOriginalScale, budDestinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);
	}
}