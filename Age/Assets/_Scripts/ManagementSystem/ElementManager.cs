using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    public enum ElementType { EARTH, FIRE, WATER, AIR, SEASON }
    [SerializeField] private BaseElement _earthElement, _fireElement, _waterElement, _airElement, _seasonElement;
    private BaseElement _selectedElement;
    [HideInInspector] public ElementType[] _currentElementOrder;
    private int _nextElementIndex = 0;

    [Header("Global Variables")]
    [HideInInspector] public GameObject[] _stemBase;
    public ParticleSystem _cloudsPT;
    public ParticleSystem _rainPT;
    public ParticleSystem _smokeTrailPT;

    [HideInInspector] public List<ParticleSystem> _bloomPT;
    [HideInInspector] public List<ParticleSystem> _dandelionBloomPT;
    [HideInInspector] public List<ParticleSystem> _dandelionBlowPT;
    [HideInInspector] public List<ParticleSystem> _dandelionStillPT;
    [HideInInspector] public List<ParticleSystem> _flowerSoilTuftPT;
    [HideInInspector] public List<ParticleSystem> _splashPT;
    [HideInInspector] public List<ParticleSystem> _stemPopPT;

    [HideInInspector] public List<Animator> _flowerAnims;

    private void Awake()
    {
        DelegatesAndEvents._onElementAcivated += ElementActivated;
    }

    private void Start()
    {


        #region Global variables
        _stemBase = GameObject.FindGameObjectsWithTag("StemBase");

        //List initialise
        _bloomPT = new List<ParticleSystem>();
        _dandelionBloomPT = new List<ParticleSystem>();
        _dandelionBlowPT = new List<ParticleSystem>();
        _dandelionStillPT = new List<ParticleSystem>();
        _flowerSoilTuftPT = new List<ParticleSystem>();
        _splashPT = new List<ParticleSystem>();
        _stemPopPT = new List<ParticleSystem>();

        _flowerAnims = new List<Animator>();

        //Particle lists
        var _findParticles = GameObject.FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem p in _findParticles)
        {
            switch (p.tag)
            {
                case ("BloomParticle"):
                    _bloomPT.Add(p);
                    break;
                case ("DandelionBloomParticle"):
                    _dandelionBloomPT.Add(p);
                    break;
                case ("DandelionBlowParticle"):
                    _dandelionBlowPT.Add(p);
                    break;
                case ("DandelionStillParticle"):
                    _dandelionStillPT.Add(p);
                    break;
                case ("SoilTuftParticle"):
                    _flowerSoilTuftPT.Add(p);
                    break;
                case ("SplashParticle"):
                    _splashPT.Add(p);
                    break;
                case ("StemPopParticle"):
                    _stemPopPT.Add(p);
                    break;
                default:
                    break;
            }
        }

        Animator _findAnimators;

        foreach (GameObject a in _stemBase)
        {
            _findAnimators = a.GetComponentInParent<Animator>();

            if (_findAnimators != null)
                _flowerAnims.Add(_findAnimators);
        }
        #endregion
    }

    public void SpawnElement()
    {
        _selectedElement.gameObject.SetActive(true);
        _selectedElement.Init();
        _nextElementIndex++;
    }

    // Selects next element relative to the Element Spawn Order.
    private void SelectNextElement()
    {
        switch(_currentElementOrder[_nextElementIndex])
        {
            case ElementType.EARTH:
                _selectedElement = _earthElement;
                break;
            case ElementType.WATER:
                _selectedElement = _waterElement;
                break;
            case ElementType.FIRE:
                _selectedElement = _fireElement;
                break;
            case ElementType.AIR:
                _selectedElement = _airElement;
                break;
            case ElementType.SEASON:
                _selectedElement = _seasonElement;
                break;
        }
    }

    public void ElementActivated()
    {
        if(_nextElementIndex < _currentElementOrder.Length)
        {
            SelectNextElement();
            SpawnElement();
        }
    }

    public void ResetElementOrder(ElementType[] order)
    {
        _currentElementOrder = order;
        _nextElementIndex = 0;

        ResetAllElements();
        SelectNextElement();
        SpawnElement();
    }

    private void ResetAllElements()
    {
        _earthElement.ResetElement();
        _waterElement.ResetElement();
        _fireElement.ResetElement();
        _airElement.ResetElement();
        _seasonElement.ResetElement();
    }

    #region Global variables

    public void ScaleDoodad(GameObject[] _objectArray, float _scaleDuration, Vector3 _scaleTarget)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(ScaleOverTime(g, _scaleDuration, _scaleTarget));
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
        while (currentTime <= duration);
    }

    public void Wiggle(GameObject[] _objectArray, float _duration, float _angle)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(RotateTo(g, _duration, _angle));
        }
    }

    private IEnumerator RotateTo(GameObject g, float _duration, float _angle)
    {
        float currentTime = 0.0f;

        do
        {
            g.transform.localEulerAngles = new Vector3(g.transform.rotation.eulerAngles.x, g.transform.rotation.eulerAngles.y, Mathf.PingPong(currentTime * 10f, _angle));

            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= _duration);
    }

    #endregion
}