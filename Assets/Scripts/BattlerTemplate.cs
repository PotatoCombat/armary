using System;
using UnityEngine;

[Serializable]
public class BattlerTemplate
{
    public BattlerType type;
    public int level;

    public Vector2 position;

    public BattlerData CreateBattlerData()
    {
        return new BattlerData();
    }
}
