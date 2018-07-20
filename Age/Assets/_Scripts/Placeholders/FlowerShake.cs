using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerShake : MonoBehaviour 
{
	[SerializeField] private float _duration;
	[SerializeField] float _distance;
	[SerializeField] private int _shakeAmount;

	void Start ()
	{
		StartCoroutine (WiggleFlower ());
	}

	private IEnumerator WiggleFlower()
	{
		Vector3 startPos = transform.position;
		Vector3 targetPos = new Vector3(transform.position.x + _distance, transform.position.y, transform.position.z);
		float step = 0.0f;
		int currentShake = 0;


		while (currentShake < _shakeAmount) 
		{
			currentShake++;

			while (step < 1) 
			{
				step += Time.deltaTime / _duration;
				transform.position = Vector3.Lerp(startPos, targetPos, step);
				yield return null;
			}

			while (step > 0) 
			{
				step -= Time.deltaTime / _duration;
				transform.position = Vector3.Lerp (startPos, targetPos, step);
				yield return null;
			}


			yield return null;
		}

		yield return null;
	}
}
