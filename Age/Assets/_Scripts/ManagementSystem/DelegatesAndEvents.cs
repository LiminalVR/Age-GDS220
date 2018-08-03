using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegatesAndEvents : MonoBehaviour {

    public delegate void ElementEventHandler();

    public static event ElementEventHandler _onElementAcivated;

    public static void ElementActivated()
    {
        if(_onElementAcivated != null)
            _onElementAcivated();
    }
}
