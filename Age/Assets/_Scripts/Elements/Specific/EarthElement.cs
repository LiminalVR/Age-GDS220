using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private Animator _campAnim;
	[SerializeField] private ParticleSystem _campSoilPT, _campSoilWeakPT;
	[SerializeField] private float _flowerClipDuration;
	[SerializeField] private Vector3 _flowerGrowthTargetScale;
	[SerializeField] private GameObject[] _flowers;
	#endregion

	#region Autumn
	//[Header("Autumn")]
	private List<ParticleSystem> _petalsPT = new List<ParticleSystem>();
	private List<ParticleSystem> _treeLeavesPT = new List<ParticleSystem>();
	#endregion

	#region Winter
	//[Header("Winter")]
	private List<ParticleSystem> _flowerSoilPT = new List<ParticleSystem>();
    private List<ParticleSystem> _flowerSoilTuftPT = new List<ParticleSystem>();
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
			case ("TreeParticle"):
				_treeLeavesPT.Add (p);
				break;
            case ("SoilTuftParticle"):
                _flowerSoilTuftPT.Add(p);
                break;
                default:
				break;
			}
		}
	}

	
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			
			EnactWinterActions (true);
		}
        if (Input.GetKey(KeyCode.Alpha2))
        {
            
            EnactWinterActions(false);
        }
    }
	

	//1: Fixes campfire and causes dirt tufts 2: Causes additional dirt tufts
    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            _campAnim.SetBool("cFireDead", false);
			_campSoilPT.Play ();
        }
        else
        {
			_campSoilWeakPT.Play ();
        }
    }

	//1: Clips off flower petals incl particles 2: Causes tree leaves to fall
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
			foreach (ParticleSystem p in _treeLeavesPT) {
				p.Play ();
			}
        }
    }

	//1: Causes dirt tufts at each flower growth position 2: ***
    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
		{
			foreach (ParticleSystem p in _flowerSoilPT)
            {
				p.Play ();
			}
        }
        else
        {
            foreach (ParticleSystem p in _flowerSoilTuftPT)
            {
                p.Play();
            }
        }
    }

	//1: Dumps soil on campfire, extinguising it and knocking down 2: *** Regrows dandelion pollen particles
    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {
			_soilDumpPT.Play ();
			_campAnim.SetBool("cFireDead", true);
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