using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private Animator _campAnim;
	[SerializeField] private ParticleSystem _campSoilPT;
	[SerializeField] private float _flowerClipDuration;
	[SerializeField] private Vector3 _flowerGrowthTargetScale;
	[SerializeField] private GameObject[] _flowers;
	#endregion

	#region Autumn
	//[Header("Autumn")]
	private List<ParticleSystem> _petalsPT;
	private List<ParticleSystem> _treeLeavesPT;
	#endregion

	#region Winter
	//[Header("Winter")]
    private List<ParticleSystem> _flowerSoilTuftPT;
    #endregion

    #region Spring
    [Header("Spring")]
	[SerializeField] private ParticleSystem _soilDumpPT;
    [SerializeField] private ParticleSystem _firePT;
	#endregion

	private void Start()
	{
        _petalsPT = new List<ParticleSystem>();
        _flowerSoilTuftPT = new List<ParticleSystem>();

		_campAnim.SetBool("cFireDead", true);

		_flowers = GameObject.FindGameObjectsWithTag("PetalsOpen");

		var _findParticles = FindObjectsOfType<ParticleSystem>();

		foreach (ParticleSystem p in _findParticles) {
			switch (p.tag) {
			case ("PetalParticle"):
				_petalsPT.Add (p);
				break;
            case ("SoilTuftParticle"):
                _flowerSoilTuftPT.Add(p);
                break;
                default:
				break;
			}
		}
	}

	//Fixes campfire and causes dirt tufts
    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            _campAnim.SetBool("cFireDead", false);
			_campSoilPT.Play ();
        }
        else
        {
		
        }
    }

	//1: Clips off flower petals incl particles 2: Causes tree leaves to fall
    protected override void EnactAutumnActions(bool initialAction)
    {
        ClipFlower();

        foreach (ParticleSystem p in _petalsPT)
        {
            p.Play();
        }

        if (initialAction)
        {
			
        }
        else
        {

        }
    }

	//Causes dirt tufts at each flower growth position
    protected override void EnactWinterActions(bool initialAction)
    {
        foreach (ParticleSystem p in _flowerSoilTuftPT)
        {
            p.Play();
        }

        if (initialAction)
		{
			
        }
        else
        {
            
        }
    }

	//Dumps soil on campfire, extinguising it and knocking down
    protected override void EnactSpringActions(bool initialAction)
    {
        _soilDumpPT.Play();
        _firePT.Stop();
        _campAnim.SetBool("cFireDead", true);

        if (initialAction)
        {
			
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