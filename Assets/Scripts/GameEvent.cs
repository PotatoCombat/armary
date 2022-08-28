using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    public interface IListener
    {
        void Notify();
    }

    private List<IListener> _listeners = new();

    public void Raise()
    {
        foreach (var listener in _listeners)
        {
            listener.Notify();
        }
    }

    public void AddListener(IListener listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(IListener listener)
    {
        _listeners.Remove(listener);
    }
}

public abstract class GameEvent<T0> : ScriptableObject
{
    public interface IListener
    {
        void Notify(T0 obj);
    }

    private List<IListener> _listeners = new();

    public void Raise(T0 obj)
    {
        foreach (var listener in _listeners)
        {
            listener.Notify(obj);
        }
    }

    public void AddListener(IListener listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(IListener listener)
    {
        _listeners.Remove(listener);
    }
}

public abstract class GameEvent<T0, T1> : ScriptableObject
{
    public interface IListener
    {
        void Notify(T0 obj, T1 obj1);
    }

    private List<IListener> _listeners = new();

    public void Raise(T0 obj, T1 obj1)
    {
        foreach (var listener in _listeners)
        {
            listener.Notify(obj, obj1);
        }
    }

    public void AddListener(IListener listener)
    {
        _listeners.Remove(listener);
        _listeners.Add(listener);
    }

    public void RemoveListener(IListener listener)
    {
        _listeners.Remove(listener);
    }
}
