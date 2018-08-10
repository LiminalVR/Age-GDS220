using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

	#region Summer
	[Header("Summer")]
    [SerializeField] private float _wiggleDuration;
    [SerializeField] private float _wiggleAngle;
    private List<ParticleSystem> _bloomPT;
    private List<ParticleSystem> _splashPT;
    [SerializeField] private ParticleSystem waterShower1, waterShower2;
    private List<Animator> _flowerAnims;
    #endregion

    #region Autumn
    [Header("Autumn")]
    [SerializeField] private ParticleSystem _rainPT;
	private ParticleSystem.MainModule _rainPTMainModule;
    private ParticleSystem.EmissionModule _rainPTEmissionModule;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] RainbowLaunch _rainbowLauncher1, _rainbowLauncher2;
    private List<ParticleSystem> _dandBloomPT;
    private List<ParticleSystem> _dandelionStillPT;
    #endregion

    #region Spring
    [Header("Spring")]
    private GameObject[] _flowerPetals;
    private GameObject[] _stemBase;
    [SerializeField] private ParticleSystem _skySparklePT;
    [SerializeField] private Vector3 _flowerStemGrowthTargetScale;
    private List<ParticleSystem> _stemPopPT;
    #endregion

    private void Start()
    {
        //List initialise
        _bloomPT = new List<ParticleSystem>();
        _splashPT = new List<ParticleSystem>();
        _flowerAnims = new List<Animator>();
        _dandBloomPT = new List<ParticleSystem>();
        _dandelionStillPT = new List<ParticleSystem>();
        _stemPopPT = new List<ParticleSystem>();

        //Rain particle declaration (for modifying its speed)
        _rainPTMainModule = _rainPT.main;

		//Object arrays
        _flowerPetals = GameObject.FindGameObjectsWithTag("Petals");
        _stemBase = GameObject.FindGameObjectsWithTag("StemBase");

        //Find animators on flowers only
        Animator _findAnimators;

        foreach (GameObject a in _stemBase)
        {
            _findAnimators = a.GetComponentInParent<Animator>();

            if (_findAnimators != null)
            _flowerAnims.Add(_findAnimators);
        }

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
            case ("DandelionStillParticle"):
                _dandelionStillPT.Add(p);
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
        waterShower1.Play();
        waterShower2.Play();

        StartCoroutine(BloomFlowers(5f));

        if (initialAction)
        {
           
        }
        else
        {
			
        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        _rainPT.Play();
        StartCoroutine(WaterRainingEffects(4.5f));

        if (initialAction)
        {
			
        }
        else
        {
			
		}
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        _rainbowLauncher1.LaunchRainbow();
        _rainbowLauncher2.LaunchRainbow();

        foreach (ParticleSystem p in _dandBloomPT)
        {
            p.Play();
        }

        foreach (ParticleSystem p in _dandelionStillPT)
        {
            p.Play();
        }

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
        foreach (ParticleSystem p in _stemPopPT)
        {
            p.Play();
        }

        foreach(ParticleSystem p in _splashPT)
            {
            p.Play();
        }

        StartCoroutine(BloomFlowers(1.4f));

        if (initialAction)
        {
            
        }
        else
        {
			
        }
    }

    private IEnumerator BloomFlowers(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Animator a in _flowerAnims)
        {
            a.SetBool("Bloomed", true);
        }

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

    /*
    private void GrowStem(GameObject[] _objectArray, float _growDuration, Vector3 _growthScale, List<ParticleSystem> _ptList)
	{
		foreach(GameObject stem in _objectArray)
		{
			StartCoroutine(ScaleOverTime(stem, _growDuration, _growthScale, _ptList));
		}
	}
    */

    private void Wiggle(GameObject[] _objectArray)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(RotateTo (g, _wiggleDuration));
        }
    }

    /*
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
    */

    private IEnumerator RotateTo (GameObject g, float time)
    {
        {
            float currentTime = 0.0f;

            {
                do
                {
                    g.transform.localEulerAngles = new Vector3(g.transform.rotation.eulerAngles.x, g.transform.rotation.eulerAngles.y, Mathf.PingPong(currentTime * 5f, _wiggleAngle));

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

        _rainPT.Emit(50);

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