using UnityEngine;

[CreateAssetMenu]
public class CactusAi : BattlerAi
{
    public MoveType attack;
    public MoveType needle;
    public MoveType heal;
    public MoveType thousandNeedles;

    public override BattleDecision GetTurnDecision(BattleContext context)
    {
        var decision = new BattleDecision();
        // if (context.User.Status[BERSERK])
        // {
        //     decision.TargetBattler = context.RandomFoe();
        //     decision.Move = attack;
        // }
        // else if (context.User.HpPercentage > 50)
        // {
        //     decision.TargetBattler = context.RandomFoe();
        //     decision.Move = needle;
        // }
        if (Random.value > 0.5f)
        {
            decision.TargetBattler = context.User;
            decision.Move = heal;
        }
        else
        {
            decision.TargetTeam = context.FoeTeam;
            decision.Move = thousandNeedles;
        }
        return decision;
    }
}
