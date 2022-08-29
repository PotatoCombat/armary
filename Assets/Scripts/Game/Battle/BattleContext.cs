using UnityEngine;

public class BattleContext : MonoBehaviour
{
    [field:Header("Runtime")]
    [field:SerializeField] public bool SurpriseAttack { get; set; }
    [field:SerializeField] public bool FinalWave { get; set; }

    [field:SerializeField] public Team AllyTeam { get; set; }
    [field:SerializeField] public Team FoeTeam { get; set; }

    [field:SerializeField] public Battler User { get; set; }
    [field:SerializeField] public MoveType Move { get; set; }

    [field:SerializeField] public Battler TargetUser { get; set; }
    [field:SerializeField] public Battler TargetBattler { get; set; }
    [field:SerializeField] public Team TargetTeam { get; set; }

    [field:Header("Components")]
    [field:SerializeField] public Team PlayerTeam { get; set; }
    [field:SerializeField] public Team NpcTeam { get; set; }
    [field:SerializeField] public Team WeatherTeam { get; set; }

    public bool AllPlayersDead => PlayerTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool AllNpcsDead => NpcTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool TurnEnded => AllyTeam.battlers.TrueForAll(battler => battler.actions == 0);

    public Battler RandomAlly()
    {
        var allies = AllyTeam.battlers.FindAll(battler => battler.isActiveAndEnabled && battler.IsAlive);
        return allies[Random.Range(0, allies.Count)];
    }

    public Battler RandomFoe()
    {
        var foes = FoeTeam.battlers.FindAll(battler => battler.isActiveAndEnabled && battler.IsAlive);
        return foes[Random.Range(0, foes.Count)];
    }
}
