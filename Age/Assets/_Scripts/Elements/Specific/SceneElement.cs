using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;

public class SceneElement : BaseElement
{
    private SeasonManager _seasonManager;

    [Header("Clouds")]
    private ParticleSystem.EmissionModule _cloudEmissionModule;
    [SerializeField] private float auRate = 0f, wnRate = 0f, spRate = 0f;

    [Header("Rain")]
    [SerializeField] private AudioSource _asRain;

    [Header("Light")]
    [SerializeField] private GameObject _sceneLight;
    [SerializeField] private Vector3 auRotate = Vector3.zero, wnRotate = Vector3.zero, spRotate = Vector3.zero;

    [Header("Leaves")]
    [SerializeField] private ParticleSystem _auTreeLeavesPT;
    [SerializeField] private ParticleSystem _auTerrainLeavesPT;

    [Header("Spring")]
    [SerializeField] private ParticleSystem _spTerrainPollenPT;

    [Header("Models")]
    [SerializeField] private float _scaleDuration = 0f;
    [SerializeField] private Vector3 _scaleTarget = Vector3.zero;

    private void Start()
    {
        _seasonManager = GameObject.FindGameObjectWithTag("GameGod").GetComponent<SeasonManager>();

        _cloudEmissionModule = _elementManager._cloudsPT.emission;
    }

    public override void Interact()
    {
        base.Interact();

        _seasonManager.BeginSeasonChange();
    }

    //Cloud rate, light rotate, begin misc leaves particles
    protected override void EnactSummerActions()
    {
        _cloudEmissionModule.rateOverTime = auRate;

        StartCoroutine (LightRotate(auRotate, 5f));

        _auTreeLeavesPT.Play();
        _auTerrainLeavesPT.Play();
    }

    //Cloud rate, light rotate, scale down flowers
    protected override void EnactAutumnActions()
    {
        _cloudEmissionModule.rateOverTime = wnRate;

        StartCoroutine(LightRotate(wnRotate, 5f));

        _auTreeLeavesPT.Stop();
        _auTerrainLeavesPT.Stop();
        
        _elementManager.ScaleDoodad(_elementManager._stemBase, _scaleDuration, _scaleTarget);
    }

    //Cloud rate, light rotate, stop misc leaves particles, begin misc pollen particles
    protected override void EnactWinterActions()
    {
        _cloudEmissionModule.rateOverTime = spRate;

        StartCoroutine(LightRotate(spRotate, 5f));

        _elementManager._rainPT.Stop();
        _asRain.Stop();
        _spTerrainPollenPT.Play(); 
    }

    protected override void EnactSpringActions()
    {
        ExperienceApp.End();
    }

    private IEnumerator LightRotate (Vector3 rotateVector, float time)
    {
        _activeCoroutines++;
        float currentTime = 0.0f;

        Quaternion RotateTo = Quaternion.Euler(new Vector3(rotateVector.x, rotateVector.y, rotateVector.z));

        do
        {
            _sceneLight.transform.rotation = Quaternion.Slerp(_sceneLight.transform.rotation, RotateTo, Time.deltaTime / time);

            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }
}