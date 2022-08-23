using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattlerTemplate
{
    public BattlerType type;
    public int level;

    public Vector2 position;
    //public float delay

    public BattlerData CreateBattlerData()
    {
        return new BattlerData()
        {
            hp = type.hp * level,
            maxHp = type.hp * level,
            moves = new List<MoveType>(type.moves),
        };
    }
}
