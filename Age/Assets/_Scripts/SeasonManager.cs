using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {
    
    
    public enum Seasons { SUMMER, WINTER, AUTUMN, SPRING }
    public static Seasons _currentSeason;

    [Header("Seasons")]
    [SerializeField] private Seasons[] _seasonOrder;
    [SerializeField] private Seasons _season;
    [SerializeField] private GameObject _summerTerrain;
    [SerializeField] private GameObject _winterTerrain;
    [SerializeField] private GameObject _autumnTerrain;
    [SerializeField] private GameObject _springTerrain;
    private int _currentSeasonNum = 0;

    [SerializeField] private float _seasonDuration;
    [SerializeField] private bool _unlimitedDuration;

    [Header("Transition")]
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Image _fadeMask;

    private void Start()
    {
        _currentSeason = _season;

        Setup();
    }

    private void Update()
    {
    }

    private void Setup()
    {
        DeactiveAllTerrains();

        switch(_currentSeason)
        {
            case Seasons.SUMMER:
                _summerTerrain.SetActive(true);
                StartCoroutine(ProgressSummer());
                break;

            case Seasons.WINTER:
                StartCoroutine(ProgressWinter());
                _winterTerrain.SetActive(true);
                break;

            case Seasons.AUTUMN:
                StartCoroutine(ProgressAutumn());
                _autumnTerrain.SetActive(true);
                break;

            case Seasons.SPRING:
                StartCoroutine(ProgressSpring());
                _springTerrain.SetActive(true);
                break;
        }

        StartCoroutine(ManipulateFadeMask(_fadeInDuration, 0));
    }

    public IEnumerator ManipulateFadeMask(float duration, float targetAlpha)
    {
        var step = 0.0f;
        Color startColour = _fadeMask.color;

        while(step < 1)
        {
            step += Time.deltaTime / duration;
            _fadeMask.color = Color.Lerp(startColour, new Color(startColour.r, startColour.g, startColour.b, targetAlpha), step);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator ChangeSeason()
    {
        StartCoroutine(ManipulateFadeMask(_fadeOutDuration, 1));

        yield return new WaitForSeconds(_fadeOutDuration);

        _currentSeasonNum++;
        _currentSeason = _seasonOrder[_currentSeasonNum];

        Setup();

        yield return null;
    }

    private IEnumerator ProgressSummer()
    {
        

        var currentTime = 0.0f;

        while(currentTime < _seasonDuration || _unlimitedDuration)
        {
            currentTime += Time.deltaTime;
            
            // Summer actions.

            yield return null;
        }

        StartCoroutine(ChangeSeason());

        yield return null;
    }

    private IEnumerator ProgressWinter()
    {
        print(_currentSeason);

        var currentTime = 0.0f;

        while(currentTime < _seasonDuration || _unlimitedDuration)
        {
            currentTime += Time.deltaTime;

            // Winter actions.

            yield return null;
        }

        StartCoroutine(ChangeSeason());
        yield return null;
    }

    private IEnumerator ProgressAutumn()
    {
        print(_currentSeason);

        var currentTime = 0.0f;

        while(currentTime < _seasonDuration || _unlimitedDuration)
        {
            currentTime += Time.deltaTime;

            // Autumn actions.

            yield return null;
        }

        StartCoroutine(ChangeSeason());
        yield return null;
    }

    private IEnumerator ProgressSpring()
    {
        print(_currentSeason);

        var currentTime = 0.0f;

        while(currentTime < _seasonDuration || _unlimitedDuration)
        {
            currentTime += Time.deltaTime;

            // Spring actions.

            yield return null;
        }

        StartCoroutine(ChangeSeason());
        yield return null;
    }

    private void DeactiveAllTerrains()
    {
        _summerTerrain.SetActive(false);
        _winterTerrain.SetActive(false);
        _autumnTerrain.SetActive(false);
        _springTerrain.SetActive(false);
    }
}
