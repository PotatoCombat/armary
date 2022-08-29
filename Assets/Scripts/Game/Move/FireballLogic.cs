using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireballLogic : MoveLogic
{
    public override List<HitCommand> CreateHits(BattleContext context)
    {
        var hits = new List<HitCommand>()
        {
            new BasicDamageCommand(context.TargetBattler, context.Move.power),
            new BasicDamageCommand(context.TargetBattler, context.Move.power),
            new BasicDamageCommand(context.TargetBattler, context.Move.power),
            new BasicDamageCommand(context.TargetBattler, context.Move.power),
        };
        return hits;
    }
}
