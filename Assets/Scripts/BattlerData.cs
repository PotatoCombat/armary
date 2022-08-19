using System;

[Serializable]
public class BattlerData
{
    public BattlerType type;
    public MoveData attack;
    public MoveData[] moves; // skills

    // public MoveData attack;
    // public MoveData[] limitBreaks;
    // Current stats
}
