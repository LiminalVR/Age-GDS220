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
    [SerializeField] private float _wiggleDuration;
    [SerializeField] private float _wiggleAngle;
    private List<ParticleSystem> _bloomPT = new List<ParticleSystem>();
	private List<ParticleSystem> _splashPT = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem waterShower1, waterShower2;
    #endregion

    #region Autumn
    [Header("Autumn")]
    [SerializeField] private ParticleSystem _rainPT;
	private ParticleSystem.MainModule _rainPTMainModule;
    private ParticleSystem.EmissionModule _rainPTEmissionModule;
    #endregion

    #region Winter
    [Header("Winter")]
	private GameObject[] _dandelionStem;
	[SerializeField] private float _dandelionGrowDuration;
	[SerializeField] private Vector3 _dandelionGrowthTargetScale;
    [SerializeField] RainbowLaunch _rainbowLauncher1, _rainbowLauncher2;
    private List<ParticleSystem> _dandBloomPT = new List<ParticleSystem>();
    #endregion

    #region Spring
    [Header("Spring")]
    [SerializeField] private ParticleSystem _skySparklePT;
    [SerializeField] private Vector3 _flowerStemGrowthTargetScale;
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
        StartCoroutine(WaterFlowers());

        if (initialAction)
        {
           
        }
        else
        {
			
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
        _rainbowLauncher1.LaunchRainbow();
        _rainbowLauncher2.LaunchRainbow();

        GrowStem(_dandelionStem, _dandelionGrowDuration, _dandelionGrowthTargetScale, _dandBloomPT);

        _rainPT.Stop();
        _skySparklePT.Play();

        if (initialAction)
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
            foreach (ParticleSystem p in _stemPopPT)
            {
                p.Play();
            }

            GrowStem (_flowerStem, _flowerGrowDuration, _flowerStemGrowthTargetScale, _bloomPT);

            foreach (GameObject flower in _flowersClose)
            {
                StartCoroutine(ScaleOverTime(flower, _flowerGrowDuration, _flowerGrowthTargetScale, _splashPT));
            }
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

    private IEnumerator WaterFlowers()
    {
        waterShower1.Play();
        waterShower2.Play();

        yield return new WaitForSeconds(5f);

        OpenFlower();

        yield return new WaitForSeconds(4.5f);

        Wiggle(_stemBase);

        foreach (ParticleSystem p in _splashPT)
        {
            p.Play();
        }
        foreach (ParticleSystem p in _bloomPT)
        {
            p.Play();
        }

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

    private void GrowStem(GameObject[] _objectArray, float _growDuration, Vector3 _growthScale, List<ParticleSystem> _ptList)
	{
		foreach(GameObject stem in _objectArray)
		{
			StartCoroutine(ScaleOverTime(stem, _growDuration, _growthScale, _ptList));
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
                    g.transform.localEulerAngles = new Vector3(g.transform.rotation.eulerAngles.x, g.transform.rotation.eulerAngles.y, Mathf.PingPong(currentTime * 20, _wiggleAngle));

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