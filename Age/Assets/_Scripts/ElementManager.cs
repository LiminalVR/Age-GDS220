using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    [SerializeField] private BaseElement[] _elementOrder;
    [SerializeField] private Transform[] _elementSpawnPoints;
    private int _nextElementIndex = 0;
    private BaseElement _lastSpawnedElement;

    /*
    struct Element {
        public BaseElement _element;
        public bool _isOn;
    }

    private Element[] _elements;
    */

    private void Awake()
    {
        DelegatesAndEvents._onElementAcivated += ElementActivated;
    }

    private void Start()
    {
        SpawnElement();
    }

    public void SpawnElement()
    {
        BaseElement element = _elementOrder[_nextElementIndex];
        Transform sp = _elementSpawnPoints[_nextElementIndex];

        _lastSpawnedElement = Instantiate(element, sp.position, sp.rotation);

        _nextElementIndex++;
    }

    public void ElementActivated()
    {
        SpawnElement();
    }

    private void Update()
    {
    }
}
