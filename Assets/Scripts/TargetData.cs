using System;

[Serializable]
public class TargetData
{
    public Type type;
    public Team team;
    public Condition condition;

    public enum Type
    {
        Self,
        Single,
        Team,
        Mixed,
        All,
    }

    public enum Team
    {
        Any,
        Allies,
        Foes,
    }

    public enum Condition
    {
        Any,
        Alive,
        Dead,
    }

    public bool TargetAllies => team != Team.Foes;
    public bool TargetFoes => team != Team.Allies;

    public bool TargetAlive => condition != Condition.Dead;
    public bool TargetDead => condition != Condition.Alive;
}
