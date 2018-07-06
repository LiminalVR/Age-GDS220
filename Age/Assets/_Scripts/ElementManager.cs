using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    [SerializeField] private BaseElement[] _elementOrder;
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
        if(_nextElementIndex < _elementOrder.Length)
            SpawnElement();
    }

    public void SpawnElement()
    {
        BaseElement element = _elementOrder[_nextElementIndex];
        element.gameObject.SetActive(true);

        _nextElementIndex++;
    }

    public void ElementActivated()
    {
        if(_nextElementIndex < _elementOrder.Length)
            SpawnElement();
    }

    private void Update()
    {
    }
}
