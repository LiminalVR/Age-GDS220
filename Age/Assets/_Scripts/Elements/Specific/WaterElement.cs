using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

	#region Summer
	[Header("Summer")]
    [SerializeField] private float _duration;
    [SerializeField] private float _angle;
    [SerializeField] private ParticleSystem waterShower1, waterShower2;
    [SerializeField] AudioClip _bloomAC;
    #endregion

    #region Autumn
    [Header("Autumn")]
	private ParticleSystem.MainModule _rainPTMainModule;
    private ParticleSystem.EmissionModule _rainPTEmissionModule;
    #endregion

    #region Winter
    [Header("Winter")]
    [SerializeField] RainbowLaunch _rainbowLauncher1, _rainbowLauncher2;
    #endregion

    #region Spring
    [Header("Spring")]
    [SerializeField] private ParticleSystem _skySparklePT;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private Vector3 _scale;
    
    #endregion

    private void Start()
    {
        //Rain particle declaration (for modifying its speed)
        _rainPTMainModule = _elementManager._rainPT.main;
    }

    //Water flowers and bloom them
    protected override void EnactSummerActions()
    {
        waterShower1.Play();
        waterShower2.Play();

        StartCoroutine(BloomFlowers(3.0f));
    }

    //Begin rain + slowmo effect
    protected override void EnactAutumnActions()
    {
        _elementManager._rainPT.Play();
        StartCoroutine(WaterRainingEffects(_duration));
    }

    //Rainbow + sparkle particles
    protected override void EnactWinterActions()
    {
        _rainbowLauncher1.LaunchRainbow();
        _rainbowLauncher2.LaunchRainbow();

        _skySparklePT.Play();
    }

    //Grow flowers back, and bloom
    protected override void EnactSpringActions()
    {
        foreach (ParticleSystem p in _elementManager._stemPopPT)
        {
            p.Play();
        }

        foreach(ParticleSystem p in _elementManager._splashPT)
        {
            p.Play();
        }

        _elementManager.ScaleDoodad(_elementManager._stemBase, _scaleDuration, _scale);

        StartCoroutine(BloomFlowers(5.0f));
    }

    private IEnumerator BloomFlowers(float _delay)
    {
        _activeCoroutines++;

        yield return new WaitForSeconds(_delay);

        foreach (Animator a in _elementManager._flowerAnims)
        {
            a.SetBool("Bloomed", true);
        }

        yield return new WaitForSeconds(2.4f);

        _as.PlayOneShot(_bloomAC);
        
        _elementManager.Wiggle(_elementManager._stemBase, _duration, _angle);

        foreach (ParticleSystem p in _elementManager._splashPT)
        {
            p.Play();
        }
        foreach (ParticleSystem p in _elementManager._bloomPT)
        {
            p.Play();
        }

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }

    private IEnumerator WaterRainingEffects (float _duration) 
	{
        _activeCoroutines++;
		float currentTime = 0.0f;

        float initialSpeed = _rainPTMainModule.simulationSpeed;

        _elementManager._rainPT.Emit(50);

       yield return new WaitForSeconds(1f);

		do {
            _rainPTMainModule.simulationSpeed = Mathf.Lerp(initialSpeed, 0.1f, currentTime);

            currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= _duration);

		_rainPTMainModule.simulationSpeed = initialSpeed;
        _activeCoroutines--;
        CalculateActiveStatus();

        yield return null;
	}
}