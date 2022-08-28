using UnityEngine;

public abstract class ActorCommand : ScriptableObject
{
    public abstract void Invoke(Actor actor);
}
