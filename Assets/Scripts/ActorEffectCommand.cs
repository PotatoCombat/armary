using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActorEffectCommand : ScriptableObject
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

    // public override void Invoke(Actor actor, BattleManager context)
    // {
        // var battlers = new List<Battler>();
        // if (context.TargetBattler)
        // {
        //     battlers.Add(context.TargetBattler);
        // }
        // if (context.TargetTeam)
        // {
        //     battlers.AddRange(context.TargetTeam.battlers);
        // }
        // var delta = 0f;
        // foreach (var battler in battlers)
        // {
        //     if (battler.isActiveAndEnabled)
        //     {
        //         battler.effects.Animate(name, delta);
        //         delta += interval;
        //     }
        // }

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
    // }
}
