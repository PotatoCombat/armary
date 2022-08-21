using UnityEngine;

[CreateAssetMenu]
public class NotifyBusyEvent : ActorEvent
{
    public override void Invoke(Actor actor, BattleManager manager)
    {
        manager.NotifyBusyActor(actor);
    }
}
