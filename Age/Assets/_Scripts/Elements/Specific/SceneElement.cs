using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : BaseElement
{
    private SeasonManager _seasonManager;
    [SerializeField] private AirElement _Air;
    [SerializeField] private EarthElement _Earth;
    [SerializeField] private FireElement _Fire;
    [SerializeField] private WaterElement _Water;

    [Header("Clouds")]
    private ParticleSystem.EmissionModule _cloudEmissionModule;
    [SerializeField] private float auRate = 0f, wnRate = 0f, spRate = 0f;

    [Header("Light")]
    [SerializeField] private GameObject _sceneLight;
    [SerializeField] private Vector3 auRotate = Vector3.zero, wnRotate = Vector3.zero, spRotate = Vector3.zero;

    [Header("Flowers")]
    [SerializeField] private float _shrinkDuration;
    [SerializeField] private Vector3 _shrinkTargetScale;

    [Header("Leaves")]
    [SerializeField] private ParticleSystem _auTreeLeavesPT;
    [SerializeField] private ParticleSystem _auTerrainLeavesPT;

    [Header("Spring")]
    [SerializeField] private ParticleSystem _spTerrainPollenPT;


    private void Start()
    {
        _seasonManager = GameObject.FindGameObjectWithTag("GameGod").GetComponent<SeasonManager>();

        _cloudEmissionModule = _Fire._cloudsPT.emission;
    }

    public override void Interact()
    {
        base.Interact();

        StartCoroutine(_seasonManager.ChangeSeason());
    }

    protected override void EnactSummerActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = auRate;

        StartCoroutine (LightRotate(auRotate, 5f));

        _auTreeLeavesPT.Play();
        _auTerrainLeavesPT.Play();
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = wnRate;

        StartCoroutine(LightRotate(wnRotate, 5f));

        GrowStem(_Water._flowerStem, _shrinkDuration, _shrinkTargetScale);
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = spRate;

        StartCoroutine(LightRotate(spRotate, 5f));

        foreach (ParticleSystem p in _Air._dandelionStillPT)
        {
            p.Play();
        }

        _auTreeLeavesPT.Stop();
        _auTerrainLeavesPT.Stop();

        _spTerrainPollenPT.Play();
    }

    protected override void EnactSpringActions(bool initialAction)
    {
		
    }

    private IEnumerator LightRotate (Vector3 rotateVector, float time)
    {
        float currentTime = 0.0f;

        Quaternion RotateTo = Quaternion.Euler(new Vector3(rotateVector.x, rotateVector.y, rotateVector.z));

        do
        {
            _sceneLight.transform.rotation = Quaternion.Slerp(_sceneLight.transform.rotation, RotateTo, Time.deltaTime / time);

            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);
    }

    private void GrowStem(GameObject[] _objectArray, float _scaleDuration, Vector3 _scale)
    {
        foreach (GameObject stem in _objectArray)
        {
            StartCoroutine(ScaleOverTime(stem, _scaleDuration, _scale));
        }
    }

    private IEnumerator ScaleOverTime(GameObject obj, float duration, Vector3 _scale)
    {
        Vector3 originalScale = obj.transform.localScale;

        float currentTime = 0.0f;

        do
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, _scale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= duration);
    }
}