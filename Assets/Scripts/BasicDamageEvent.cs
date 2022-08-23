using UnityEngine;

[CreateAssetMenu]
public class BasicDamageEvent : HitEvent
{
    public override void Invoke(Battler battler, BattleManager manager)
    {
        if (battler.isActiveAndEnabled)
        {
            battler.ReceiveDamage(new DamageData()
            {
                value = -manager.Move.power,
            });
        }
    }
}
