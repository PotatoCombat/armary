using System;
using UnityEngine;

[Serializable]
public class BattleDecision
{
    [field:SerializeField] public MoveType Move { get; set; }
    [field:SerializeField] public Battler TargetBattler { get; set; }
    [field:SerializeField] public Team TargetTeam { get; set; }
}
