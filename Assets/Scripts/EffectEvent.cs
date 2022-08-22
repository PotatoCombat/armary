using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EffectEvent : ActorEvent
{
    private enum Type
    {
        Single,
        Team,
        All,
        Random,
        RandomAlly,
        RandomFoe,
    }

    [SerializeField] private Type target;
    [SerializeField] private float interval;

    public override void Invoke(Actor actor, BattleManager manager)
    {
        var battlers = new List<Battler>();
        if (manager.context.targetBattler)
        {
            battlers.Add(manager.context.targetBattler);
        }
        if (manager.context.targetTeam)
        {
            battlers.AddRange(manager.context.targetTeam.battlers);
        }
        var delta = 0f;
        foreach (var battler in battlers)
        {
            battler.effects.Animate(name, delta);
            delta += interval;
        }

        // switch (target)
        // {
            // case Type.Random:
            //     // manager.RandomBattler().PlayEffect(name);
            //     break;
            // case Type.RandomAlly:
            //     // manager.RandomAlly().PlayEffect(name);
            //     break;
            // case Type.RandomFoe:
            //     // manager.RandomFoe().PlayEffect(name);
            //     break;
            // case Type.Single:
            //     manager.targets[0].PlayEffect(name);
            //     break;
            // case Type.Team:
            //
            //     break;
            // case Type.All:
            // default:
            //     break;
        // }
    }
}
