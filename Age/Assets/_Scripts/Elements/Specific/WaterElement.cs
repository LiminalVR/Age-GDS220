using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private float _flowerGrowDuration;
	[SerializeField] private Vector3 _flowerGrowthTargetScale;
	[SerializeField] private float _flowerShrinkDuration;
	[SerializeField] private Vector3 _flowerShrinkTargetScale;
	private GameObject[] _flowersOpen;
	private GameObject[] _flowersClose;
    private GameObject[] _stemBase;
	private List<ParticleSystem> _bloomPT = new List<ParticleSystem>();
	private List<ParticleSystem> _splashPT = new List<ParticleSystem>();
	#endregion

	#region Autumn
	[SerializeField] private ParticleSystem _rainPT;
	private ParticleSystem.MainModule _rainPTModule;
	#endregion

	#region Winter
	[Header("Winter")]
	[SerializeField] private GameObject[] _dandelionStem;
	[SerializeField] private float _dandelionGrowDuration;
	[SerializeField] private Vector3 _dandelionGrowthTargetScale;
	#endregion

	#region Spring
	//[Header("Spring")]
	private GameObject[] _flowerStem;
    private List<ParticleSystem> _stemPopPT = new List<ParticleSystem>();
    #endregion

    private void Start()
    {
		//Rain particle declaration (for modifying its speed)
		_rainPTModule = _rainPT.main;

		//Object arrays
        _flowersOpen = GameObject.FindGameObjectsWithTag("PetalsOpen");
		_flowersClose = GameObject.FindGameObjectsWithTag("PetalsClosed");
        _stemBase = GameObject.FindGameObjectsWithTag("StemBase");
		_dandelionStem = GameObject.FindGameObjectsWithTag("DandelionStem");
		_flowerStem = GameObject.FindGameObjectsWithTag("FlowerStem");

		//Particle lists
		var _findParticles = GameObject.FindObjectsOfType<ParticleSystem> ();

		foreach (ParticleSystem p in _findParticles) {
			switch (p.tag) {
			case ("BloomParticle"):
				_bloomPT.Add (p);
				break;
			case ("SplashParticle"):
				_splashPT.Add (p);
				break;
            case ("StemPopParticle"):
                _stemPopPT.Add(p);
                break;
                default:
				break;
			}
		}
    }

	
	//TEMPORARY TESTER
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) 
		{

			EnactSummerActions (true);
		}
		if (Input.GetKey(KeyCode.Alpha2))
		{

			EnactSummerActions(false);
		}
	}
	

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            OpenFlower();
        }
        else
        {
			foreach (ParticleSystem p in _splashPT) {
				p.Play ();
			}
			foreach (ParticleSystem p in _bloomPT) {
				p.Play ();
			}

            Wiggle(_stemBase);
        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {
			_rainPT.Play ();
        }
        else
        {
			StartCoroutine (WaterRainingEffects (4.5f));
		}
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
        {
			//Rainbow here
			GrowStem (_dandelionStem);
			_rainPT.Stop ();
        }
        else
        {
			//Brighten rainbow
        }
    }

    protected override void EnactSpringActions(bool initialAction)
    {
        if(initialAction)
        {
            foreach (ParticleSystem p in _stemPopPT)
            {
                p.Play();
            }

            GrowStem (_flowerStem);
        }
        else
        {
			foreach (ParticleSystem p in _splashPT)
            {
				p.Play ();
			}

			OpenFlower();
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

	void GrowStem(GameObject[] _objectArray)
	{
		foreach(GameObject stem in _objectArray)
		{
			StartCoroutine(ScaleOverTime(stem, _dandelionGrowDuration, _dandelionGrowthTargetScale));
		}
	}

    void Wiggle(GameObject[] _objectArray)
    {
        foreach(GameObject g in _stemBase)
        {
            StartCoroutine(StemRotate(1.8f));
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

    private IEnumerator StemRotate (float time)
    {
        {

            float currentTime = 0.0f;

            yield return new WaitForSeconds(0.8f);

            do
            {
                
              
                currentTime += Time.deltaTime;
                yield return null;
            }
            while (currentTime <= time);

        }
    }

    private IEnumerator WaterRainingEffects (float time) 
	{

		float currentTime = 0.0f;

		yield return new WaitForSeconds(1.5f);

		_rainPTModule.simulationSpeed = 0.3f;

		do {
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);

		_rainPTModule.simulationSpeed = 1f;
	}
}