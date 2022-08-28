using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattlerType : ScriptableObject
{
    // Base Stats
    public int hp;
    public List<MoveType> moves;
    public BattlerAi ai;
}
