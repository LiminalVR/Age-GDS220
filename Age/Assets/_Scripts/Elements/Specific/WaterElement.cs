using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

	#region Summer
	[Header("Flowers")]
	[SerializeField] private float _flowerGrowDuration, _flowerShrinkDuration = 0f;
	[SerializeField] private Vector3 _flowerGrowthTargetScale, _flowerShrinkTargetScale = Vector3.zero;
	[SerializeField] private GameObject[] _flowersOpen;
	[SerializeField] private GameObject[] _flowersClose;
	[SerializeField] private List<ParticleSystem> _bloomPT = new List<ParticleSystem>();
	#endregion

	#region Autumn

	#endregion

	#region Winter

	#endregion

	#region Spring

	#endregion

    private void Start()
    {
        _flowersOpen = GameObject.FindGameObjectsWithTag("PetalsOpen");
		_flowersClose = GameObject.FindGameObjectsWithTag("PetalsClosed");

		var _findBloomPT = GameObject.FindObjectsOfType<ParticleSystem> ();

		foreach (ParticleSystem p in _findBloomPT) {
			if (p.CompareTag ("BloomParticle")) {
				_bloomPT.Add (p);
			}
		}
    }

	/*
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {

			EnactSummerActions (true);
		}
	}
	*/


    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            OpenFlower();
        }
        else
        {

        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    void OpenFlower()
    {
        foreach(GameObject flower in _flowersOpen)
        {
            StartCoroutine(ScaleOverTime(flower, _flowerGrowDuration, _flowerGrowthTargetScale));
        }

		foreach(GameObject flower in _flowersClose)
		{
			StartCoroutine(ScaleOverTime(flower, _flowerShrinkDuration, _flowerShrinkTargetScale));
		}
    }

    private IEnumerator ScaleOverTime(GameObject obj, float duration, Vector3 scale)
    {
        Vector3 originalScale = obj.transform.localScale;

        float currentTime = 0.0f;

        do
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, scale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while(currentTime <= duration);

		foreach (ParticleSystem p in _bloomPT) {
			p.Play ();
		}
    }
}