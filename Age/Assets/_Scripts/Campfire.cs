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
		if (Input.GetKeyDown (KeyCode.Space)) {
			// Get the system and the emission module.
			myParticleSystem = GetComponent<ParticleSystem> ();
			emissionModule = myParticleSystem.emission;
			ParticleSystem.ShapeModule shape = myParticleSystem.shape;

			GetValue ();
			SetValue ();
			shape.radius = 0.7f;
	}
}
	void GetValue()
	{
		//print("The constant value is " + emissionModule.rate.constant);
	}

	void SetValue()
	{
		emissionModule.rate = 100.0f;
	}
}