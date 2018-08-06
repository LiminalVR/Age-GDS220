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
    [SerializeField] private GameObject[] _stemBase;
    [SerializeField] private float _wiggleDuration;
    [SerializeField] private float _wiggleAngle;
    private List<ParticleSystem> _bloomPT = new List<ParticleSystem>();
	private List<ParticleSystem> _splashPT = new List<ParticleSystem>();
    [SerializeField] WateringHose _wateringHose;
	#endregion

	#region Autumn
	[SerializeField] private ParticleSystem _rainPT;
	private ParticleSystem.MainModule _rainPTMainModule;
    private ParticleSystem.EmissionModule _rainPTEmissionModule;
    #endregion

    #region Winter
    [Header("Winter")]
	[SerializeField] private GameObject[] _dandelionStem;
	[SerializeField] private float _dandelionGrowDuration;
	[SerializeField] private Vector3 _dandelionGrowthTargetScale;
    [SerializeField] RainbowLaunch _rainbowLaunch;
    private List<ParticleSystem> _dandBloomPT = new List<ParticleSystem>();
    #endregion

    #region Spring
    [Header("Spring")]
    [SerializeField] private ParticleSystem _skySparklePT;
    [HideInInspector] public GameObject[] _flowerStem;
    private List<ParticleSystem> _stemPopPT = new List<ParticleSystem>();
    #endregion

    private void Start()
    {
		//Rain particle declaration (for modifying its speed)
		_rainPTMainModule = _rainPT.main;

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
            case ("DandBloomParticle"):
                _dandBloomPT.Add(p);
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

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            StartCoroutine(WaterHose());
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
            _rainbowLaunch.LaunchRainbow();
			GrowStem (_dandelionStem);
			_rainPT.Stop ();
        }
        else
        {
            _skySparklePT.Play();
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

    private IEnumerator WaterHose()
    {
        _wateringHose.BeginEffect();

        yield return new WaitForSeconds(15f);

        OpenFlower();

        yield return null;
    }

    private void OpenFlower()
    {
        foreach(GameObject flower in _flowersOpen)
        {
            StartCoroutine(ScaleOverTime(flower, _flowerGrowDuration, _flowerGrowthTargetScale, _bloomPT));
        }

		foreach(GameObject flower in _flowersClose)
		{
			StartCoroutine(ScaleOverTime(flower, _flowerShrinkDuration, _flowerShrinkTargetScale, _bloomPT));
		}
    }

    private void GrowStem(GameObject[] _objectArray)
	{
		foreach(GameObject stem in _objectArray)
		{
			StartCoroutine(ScaleOverTime(stem, _dandelionGrowDuration, _dandelionGrowthTargetScale, _dandBloomPT));
		}
	}

    private void Wiggle(GameObject[] _objectArray)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(RotateTo (g, _wiggleDuration));
        }
    }

    private IEnumerator ScaleOverTime(GameObject obj, float duration, Vector3 scale, List<ParticleSystem> _ptList)
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

		foreach (ParticleSystem p in _ptList) {
			p.Play ();
		}
    }

    private IEnumerator RotateTo (GameObject g, float time)
    {
        {
            float currentTime = 0.0f;

            {
                do
                {
                    g.transform.localEulerAngles = new Vector3(g.transform.rotation.eulerAngles.x, g.transform.rotation.eulerAngles.y, Mathf.PingPong(currentTime * 50, _wiggleAngle));

                    currentTime += Time.deltaTime;
                    yield return null;
                }
                    while (currentTime <= time);
            }
        }
    }

    private IEnumerator WaterRainingEffects (float time) 
	{

		float currentTime = 0.0f;

        float initialSpeed = _rainPTMainModule.simulationSpeed;

        _rainPT.Emit(100);

       yield return new WaitForSeconds(1f);

		do {
            _rainPTMainModule.simulationSpeed = Mathf.Lerp(initialSpeed, 0.1f, currentTime);

            currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);

		_rainPTMainModule.simulationSpeed = initialSpeed;
	}
}