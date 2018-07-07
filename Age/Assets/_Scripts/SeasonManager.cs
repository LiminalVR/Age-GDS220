using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {
    
    
    public enum Seasons { SUMMER, WINTER, AUTUMN, SPRING }
    public static Seasons _currentSeason;

    [Header("Seasons")]
    [SerializeField] private int _nextSceneIndex;
    [SerializeField] private Seasons _season;

    [SerializeField] private float _seasonDuration;
    [SerializeField] private bool _unlimitedDuration;

    [Header("Transition")]
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Image _fadeMask;

    private void Start()
    {
        StartCoroutine(ManipulateFadeMask(_fadeInDuration, 0));
        _currentSeason = _season;

        Setup();
    }

    private void Update()
    {
    }

    private void Setup()
    {
        switch(_currentSeason)
        {
            case Seasons.SUMMER:
                StartCoroutine(ProgressSummer());

                break;

            case Seasons.WINTER:
                StartCoroutine(ProgressWinter());

                break;

            case Seasons.AUTUMN:
                StartCoroutine(ProgressAutumn());
                break;

            case Seasons.SPRING:
                StartCoroutine(ProgressSpring());
                break;
        }
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

        SceneManager.LoadScene(_nextSceneIndex);

        yield return null;
    }

    private IEnumerator ProgressSummer()
    {
        print(_currentSeason);

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
}
