using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HitEvent : ScriptableObject
{
    public int damageModifier;

    public void Execute(List<Battler> battlers, BattleManager manager)
    {

    }
}
