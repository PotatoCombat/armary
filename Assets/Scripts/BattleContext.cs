using UnityEngine;

public class BattleContext : MonoBehaviour
{
    [field:SerializeField] public bool SurpriseAttack { get; set; }
    [field:SerializeField] public bool FinalWave { get; set; }

    [field:SerializeField] public Team PlayerTeam { get; set; }
    [field:SerializeField] public Team NpcTeam { get; set; }
    [field:SerializeField] public Team WeatherTeam { get; set; }

    [field:SerializeField] public Team AllyTeam { get; set; }
    [field:SerializeField] public Team FoeTeam { get; set; }

    [field:SerializeField] public Battler User { get; set; }
    [field:SerializeField] public MoveType Move { get; set; }

    [field:SerializeField] public Battler TargetBattler { get; set; }
    [field:SerializeField] public Team TargetTeam { get; set; }

    public bool AllPlayersDead => PlayerTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool AllNpcsDead => NpcTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool TurnEnded => AllyTeam.battlers.TrueForAll(battler => battler.actions == 0);
}
