using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    private List<Action> _listeners = new();

    public void Raise()
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke();
        }
    }

    public void AddListener(Action listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(Action listener)
    {
        _listeners.Remove(listener);
    }
}

public abstract class GameEvent<T0> : ScriptableObject
{
    private List<Action<T0>> _listeners = new();

    public void Raise(T0 obj)
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke(obj);
        }
    }

    public void AddListener(Action<T0> listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(Action<T0> listener)
    {
        _listeners.Remove(listener);
    }
}

public abstract class GameEvent<T0, T1> : ScriptableObject
{
    private List<Action<T0, T1>> _listeners = new();

    public void Raise(T0 obj, T1 obj1)
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke(obj, obj1);
        }
    }

    public void AddListener(Action<T0, T1> listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(Action<T0, T1> listener)
    {
        _listeners.Remove(listener);
    }
}
