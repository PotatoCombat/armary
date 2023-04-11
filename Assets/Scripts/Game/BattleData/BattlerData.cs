using System;
using System.Collections.Generic;

[Serializable]
public class BattlerData
{
    public BattlerType type;
    public MoveType attack;
    public List<MoveType> moves; // skills

    // public MoveData attack;
    // public MoveData[] limitBreaks;

    // Current stats
    public int hp;
    public int maxHp;
}
