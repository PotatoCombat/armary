using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameEventListener<TEvent> : GameEvent.IListener where TEvent : GameEvent
{
    [field:SerializeField, HideInInspector] public string Name { get; private set; }
    [field:SerializeField] public TEvent Event { get; private set; }
    [field:SerializeField] public UnityEvent Response { get; private set; }

    public void OnEnable()
    {
        Event.AddListener(this);
    }

    public void OnDisable()
    {
        Event.AddListener(this);
    }

    public void OnValidate()
    {
        Name = Event ? Event.name : "Unassigned";
    }

    public void Notify()
    {
        Response.Invoke();
    }
}

[Serializable]
public class GameEventListener<TEvent, T0> : GameEvent<T0>.IListener where TEvent : GameEvent<T0>
{
    [field:SerializeField, HideInInspector] public string Name { get; private set; }
    [field:SerializeField] public TEvent Event { get; private set; }
    [field:SerializeField] public UnityEvent<T0> Response { get; private set; }

    public void OnEnable()
    {
        Event.AddListener(this);
    }

    public void OnDisable()
    {
        Event.AddListener(this);
    }

    public void OnValidate()
    {
        Name = Event ? Event.name : "Unassigned";
    }

    public void Notify(T0 obj)
    {
        Response.Invoke(obj);
    }
}

[Serializable]
public class GameEventListener<TEvent, T0, T1> : GameEvent<T0, T1>.IListener where TEvent : GameEvent<T0, T1>
{
    [field:SerializeField, HideInInspector] public string Name { get; private set; }
    [field:SerializeField] public TEvent Event { get; private set; }
    [field:SerializeField] public UnityEvent<T0, T1> Response { get; private set; }

    public void OnEnable()
    {
        Event.AddListener(this);
    }

    public void OnDisable()
    {
        Event.AddListener(this);
    }

    public void OnValidate()
    {
        Name = Event ? Event.name : "Unassigned";
    }

    public void Notify(T0 obj, T1 obj1)
    {
        Response.Invoke(obj, obj1);
    }
}
