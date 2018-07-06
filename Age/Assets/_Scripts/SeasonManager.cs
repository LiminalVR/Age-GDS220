using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour {
    
    public enum Seasons { SUMMER, WINTER, AUTUMN, SPRING }
    public static Seasons _currentSeason;
    [SerializeField] private Seasons[] _seasonOrder;
    private int _currentSeasonIndex;

    [SerializeField] private float _seasonDuration;
    [SerializeField] private bool _loopSeasons;
    [SerializeField] private bool _unlimitedDuration;

    private Coroutine _summer;
    private Coroutine _winter;
    private Coroutine _autumn;
    private Coroutine _spring;

    private void Start()
    {
        _currentSeason = _seasonOrder[0];
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
                if(_summer == null)
                {
                    _summer = StartCoroutine(ProgressSummer());
                }
                break;

            case Seasons.WINTER:
                if(_winter == null)
                {
                    _winter = StartCoroutine(ProgressWinter());
                }
                break;

            case Seasons.AUTUMN:
                if(_autumn == null)
                {
                    _autumn = StartCoroutine(ProgressAutumn());
                }
                break;

            case Seasons.SPRING:
                if(_spring == null)
                {
                    _spring = StartCoroutine(ProgressSpring());
                }
                break;
        }
    }

    public void ChangeSeason()
    {
        _currentSeasonIndex = _currentSeasonIndex < _seasonOrder.Length - 1 ? _currentSeasonIndex + 1 : 0;
        _currentSeason = _seasonOrder[_currentSeasonIndex];


        if(_loopSeasons)
            WipeSeasons();

        Setup();
    }

    private void WipeSeasons()
    {
        _summer = null;
        _autumn = null;
        _winter = null;
        _spring = null;

        StopAllCoroutines();
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

        ChangeSeason();

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

        ChangeSeason();

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

        ChangeSeason();

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

        ChangeSeason();

        yield return null;
    }
}
