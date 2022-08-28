using System;
using UnityEngine;

[CreateAssetMenu]
public class SignalEvent : GameEvent<GameObject> { }

[Serializable]
public class SignalEventListener : GameEventListener<SignalEvent, GameObject> { }
