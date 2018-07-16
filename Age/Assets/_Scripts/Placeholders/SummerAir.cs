using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerAir : MonoBehaviour {

	Terrain myTerrain;

	void Start () {
		myTerrain = GetComponent<Terrain> ();
		myTerrain.terrainData.wavingGrassStrength = 0.3f;
	}

	void Update () {
		if (Input.GetKey(KeyCode.Alpha4)) {
			//Air
			StartCoroutine (WindGust(4));
		}
		//Debug.Log (myTerrain.terrainData.wavingGrassStrength);
	}

	private IEnumerator WindGust (float time) {

		float currentTime = 0.0f;

		do {
			myTerrain.terrainData.wavingGrassStrength = 1f;
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);
		//Resets wind power to 0.3 after gusting complete
		myTerrain.terrainData.wavingGrassStrength = 0.3f;
	}
}