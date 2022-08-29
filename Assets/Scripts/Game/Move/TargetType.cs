using System;

[Serializable]
public class TargetType
{
    public TargetGroup group;
    public TargetTeam team;
    public TargetCondition condition;

    public bool TargetAllies => team != TargetTeam.Foes;
    public bool TargetFoes => team != TargetTeam.Allies;

    public bool TargetAlive => condition != TargetCondition.Dead;
    public bool TargetDead => condition != TargetCondition.Alive;
}

public enum TargetGroup
{
    Self,
    Single,
    Team,
    Mixed,
    All,
}

public enum TargetTeam
{
    Any,
    Allies,
    Foes,
}

public enum TargetCondition
{
    Any,
    Alive,
    Dead,
}
