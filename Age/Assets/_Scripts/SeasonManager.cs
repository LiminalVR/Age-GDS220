using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour {
    
    public enum Seasons { SUMMER, WINTER, AUTUMN, SPRING }
    public Seasons _currentSeason;
    [SerializeField] private Seasons[] _seasonOrder;

    [SerializeField] private float _seasonDuration;

    private Coroutine _summer;
    private Coroutine _winter;
    private Coroutine _autumn;
    private Coroutine _spring;



    private void Update()
    {

    }


    private void ChangeSeason()
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

    private IEnumerator ProgressSummer()
    {
        var currentTime = 0.0f;

        while(currentTime < _seasonDuration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private IEnumerator ProgressWinter()
    {
        var currentTime = 0.0f;

        while(currentTime < _seasonDuration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private IEnumerator ProgressAutumn()
    {
        var currentTime = 0.0f;

        while(currentTime < _seasonDuration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private IEnumerator ProgressSpring()
    {
        var currentTime = 0.0f;

        while(currentTime < _seasonDuration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
