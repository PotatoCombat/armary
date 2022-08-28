using UnityEngine;

public abstract class BattlerAi : ScriptableObject
{
    public virtual BattleDecision GetTurnDecision(BattleContext context)
    {
        return null;
    }

    public virtual BattleDecision GetCounterDecision(BattleContext context)
    {
        return null;
    }
}
