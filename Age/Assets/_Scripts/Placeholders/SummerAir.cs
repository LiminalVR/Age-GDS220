using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerAir : MonoBehaviour {

	Terrain myTerrain;

	void Start () {
		myTerrain = GetComponent<Terrain> ();
	}

	void Update () {
		if (Input.GetKey(KeyCode.Alpha4)) {
			//Air
			myTerrain.terrainData.wavingGrassStrength = 1f;
		}
	}
}
