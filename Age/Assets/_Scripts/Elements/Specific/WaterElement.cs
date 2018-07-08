using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : BaseElement {

    [Header("Flowers")]
    [SerializeField] private float _flowerGrowDuration;
    [SerializeField] private Vector3 _flowerGrowthTargetScale;
    [SerializeField] private GameObject[] _flowers;


    private void Start()
    {
        _flowers = GameObject.FindGameObjectsWithTag("Petals");
    }

    protected override void EnactSummerActions(bool initialAction)
    {
        if(initialAction)
        {
            OpenFlower();
        }
        else
        {

        }
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        if(initialAction)
        {

        }
        else
        {

        }
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        if(initialAction)
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

        }
        else
        {

        }
    }

    void OpenFlower()
    {
        foreach(GameObject flower in _flowers)
        {
            StartCoroutine(ScaleOverTime(flower, _flowerGrowDuration, _flowerGrowthTargetScale));
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
        while(currentTime <= duration);
    }
}
