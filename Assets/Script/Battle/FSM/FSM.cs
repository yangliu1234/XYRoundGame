using System;
using System.Collections;
using UnityEngine;

public class FSM<T> : MonoBehaviour where T :  IComparable
{
    public delegate void StateChangEvent();
    public event Action stateChange;

    private bool isStateChange = false;
    private T _state;
    public T state 
    {
        get { return _state; }
        set {
            _state = value;
            CheckStateChange();
        }
    }
    private T _lastState;
    public T lastState 
    {
        get { return _lastState; }
    }
    private void CheckStateChange()
    {
        if (_state.CompareTo(_lastState) != 0)
        {
            isStateChange = true;
        }
    }

    private void Update()
    {
        if (isStateChange)
        {
            T nowState = state;
            if (stateChange != null)
                stateChange();
            _lastState = nowState;
            isStateChange = false;
        }
    }


}
