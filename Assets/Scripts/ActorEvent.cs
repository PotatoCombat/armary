using UnityEngine;

public abstract class ActorEvent : ScriptableObject
{
    public abstract void Invoke(Actor actor, BattleManager manager);
}
