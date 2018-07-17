using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    public enum ElementType { EARTH, FIRE, WATER, AIR, SEASON }
    [SerializeField] private BaseElement _earthElement, _fireElement, _waterElement, _airElement, _seasonElement;
    private BaseElement _selectedElement;
    public ElementType[] _currentElementOrder;
    private int _nextElementIndex = 0;

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
        print("Element Activated: " + _selectedElement);

        if(_nextElementIndex < _currentElementOrder.Length)
        {
            print("Spawning next element...");
            SelectNextElement();
            SpawnElement();
        }
    }

    public void ResetElementOrder(ElementType[] order)
    {
        _currentElementOrder = order;
        _nextElementIndex = 0;

        DeactivateAllElements();
        SelectNextElement();
        SpawnElement();
    }

    private void DeactivateAllElements()
    {
        _earthElement.gameObject.SetActive(false);
        _waterElement.gameObject.SetActive(false);
        _fireElement.gameObject.SetActive(false);
        _airElement.gameObject.SetActive(false);
        _seasonElement.gameObject.SetActive(false);
    }
}
