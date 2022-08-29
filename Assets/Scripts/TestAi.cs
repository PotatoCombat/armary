using UnityEngine;

[CreateAssetMenu]
public class TestAi : BattlerAi
{
    public MoveType fireball;

    public override BattleDecision GetTurnDecision(BattleContext context)
    {
        var decision = new BattleDecision();
        decision.Move = fireball;
        decision.TargetBattler = context.RandomFoe();
        return decision;
    }
}
