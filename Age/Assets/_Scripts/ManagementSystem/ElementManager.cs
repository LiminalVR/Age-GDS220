using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    public enum ElementType { EARTH, FIRE, WATER, AIR, SEASON }
    [SerializeField] private BaseElement _earthElement, _fireElement, _waterElement, _airElement, _seasonElement;
    private BaseElement _selectedElement;
    [HideInInspector] public ElementType[] _currentElementOrder;
    private int _nextElementIndex = 0;

    
    [Header("Sizes")]
    [SerializeField] private Vector3 _normalSize;
    [SerializeField] private Vector3 _shrunkenSize;
    [SerializeField] private Vector3 _enlargedSize;
    [SerializeField] private float _adjustmentDuration;
    [SerializeField] private float _stagnentDuration;
    

    private List<BaseElement> _elementList;

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
        _elementList = new List<BaseElement>();
        _elementList.Add(_earthElement);
        _elementList.Add(_waterElement);
        _elementList.Add(_fireElement);
        _elementList.Add(_airElement);
    }

    public void SpawnElement()
    {
        _selectedElement.gameObject.SetActive(true);
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
        StartCoroutine(AdjustElements(_currentElementOrder[_nextElementIndex - 1], _selectedElement));

        if(_nextElementIndex < _currentElementOrder.Length)
        {
            SelectNextElement();
            SpawnElement();
        }
    }

    
    private IEnumerator AdjustElements(ElementType activatedType, BaseElement activatedElement)
    {
        // Shrinking.
        if(activatedType != ElementType.EARTH)
            StartCoroutine(ResizeElement(_earthElement, _shrunkenSize, _adjustmentDuration));

        if(activatedType != ElementType.WATER)
            StartCoroutine(ResizeElement(_waterElement, _shrunkenSize, _adjustmentDuration));

        if(activatedType != ElementType.FIRE)
            StartCoroutine(ResizeElement(_fireElement, _shrunkenSize, _adjustmentDuration));

        if(activatedType != ElementType.AIR)
            StartCoroutine(ResizeElement(_airElement, _shrunkenSize, _adjustmentDuration));

        if(activatedType != ElementType.SEASON)
            StartCoroutine(ResizeElement(_seasonElement, _shrunkenSize, _adjustmentDuration));

        // Enlarging.
        StartCoroutine(ResizeElement(activatedElement, _enlargedSize, _adjustmentDuration));

        yield return new WaitForSeconds(_adjustmentDuration + _stagnentDuration);

        // Returning.
        foreach(BaseElement element in _elementList)
        {
            StartCoroutine(ResizeElement(element, _normalSize, _adjustmentDuration));
        }

        yield return null;
    }
    
    private IEnumerator ResizeElement(BaseElement targetElement, Vector3 targetSize, float duration)
    {
        Vector3 startSize = targetElement.gameObject.transform.localScale;
        Vector3 stepSize = Vector3.zero;
        float step = 0.0f;

        while(step < 1.0f)
        {
            stepSize = Vector3.Lerp(startSize, targetSize, step);
            step += Time.deltaTime / duration;

            targetElement.gameObject.transform.localScale = stepSize;

            yield return null;
        }

        yield return null;
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
}
