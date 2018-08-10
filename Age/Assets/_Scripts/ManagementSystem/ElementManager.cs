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

    private void Awake()
    {
        DelegatesAndEvents._onElementAcivated += ElementActivated;
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
}
