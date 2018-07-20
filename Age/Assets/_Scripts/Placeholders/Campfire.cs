using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour 
{
	ParticleSystem myParticleSystem;
	ParticleSystem.EmissionModule emissionModule;
	ParticleSystem.ShapeModule shape;


	void Update()
	{
		if (Input.GetKey (KeyCode.Space)) {
			// Get the system and the emission module.
			myParticleSystem = GetComponent<ParticleSystem> ();
			emissionModule = myParticleSystem.emission;
			ParticleSystem.ShapeModule shape = myParticleSystem.shape;

			shape.radius = 0.7f;
			emissionModule.rate = 100.0f;
		} else {
			myParticleSystem = GetComponent<ParticleSystem> ();
			emissionModule = myParticleSystem.emission;
			ParticleSystem.ShapeModule shape = myParticleSystem.shape;

			shape.radius = 0.5f;
			emissionModule.rate = 20.0f;
		}
	}
}