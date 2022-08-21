using UnityEngine;

[CreateAssetMenu]
public class NotifyIdleEvent : ActorEvent
{
    public override void Invoke(Actor actor, BattleManager manager)
    {
        manager.NotifyIdleActor(actor);
    }
}
