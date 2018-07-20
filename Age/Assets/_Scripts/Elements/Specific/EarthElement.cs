using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private Animator _campAnim;
	[SerializeField] private ParticleSystem _campSoilPT, _campSoilDirtPT;
	[SerializeField] private float _flowerClipDuration;
	[SerializeField] private Vector3 _flowerGrowthTargetScale;
	[SerializeField] private GameObject[] _flowers;
	#endregion

	#region Autumn
	[Header("Autumn")]
	[SerializeField] private List<ParticleSystem> _petalsPT = new List<ParticleSystem>();
	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private List<ParticleSystem> _flowerSoilPT = new List<ParticleSystem>();
	#endregion

	#region Spring
	[Header("Spring")]
	[SerializeField] private ParticleSystem _soilDumpPT;
	#endregion

	private void Start()
	{
		_campAnim.SetBool("cFireDead", true);

		_flowers = GameObject.FindGameObjectsWithTag("PetalsOpen");

		var _findParticles = GameObject.FindObjectsOfType<ParticleSystem> ();

		foreach (ParticleSystem p in _findParticles) {
			switch (p.tag) {
			case ("PetalParticle"):
				_petalsPT.Add (p);
				break;
			case ("SoilParticle"):
				_flowerSoilPT.Add (p);
				break;
			default:
				break;
			}
		}
	}

	/*
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			
			EnactWinterActions (true);
		}
	}
	*/

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            _campAnim.SetBool("cFireDead", false);
			_campSoilPT.Play ();
        }
        else
        {
			_campSoilDirtPT.Play ();
        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {
			ClipFlower ();

			foreach (ParticleSystem p in _petalsPT) {
					p.Play ();
			}
        }
        else
        {

        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
		{
			foreach (ParticleSystem p in _flowerSoilPT) {
				p.Play ();
			}
        }
        else
        {

        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {
			_soilDumpPT.Play ();
        }
        else
        {

        }
    }

	void ClipFlower()
	{
		foreach(GameObject flower in _flowers)
		{
			StartCoroutine(ScaleOverTime(flower, _flowerClipDuration, _flowerGrowthTargetScale));
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
	}
}