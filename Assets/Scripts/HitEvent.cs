using UnityEngine;

public abstract class HitEvent : ScriptableObject
{
    public virtual void Invoke(Battler battler, BattleManager manager) { }

    public virtual void Invoke(Team team, BattleManager manager)
    {
        foreach (var battler in team.battlers)
        {
            Invoke(battler, manager);
        }
    }
}
