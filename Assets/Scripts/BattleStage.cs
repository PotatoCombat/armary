using System;
using UnityEngine;

[Serializable]
public class BattleStage : MonoBehaviour
{
    [Header("Runtime")]
    public Team allyTeam;
    public Team foeTeam;

    [Header("Components")]
    public Team playerTeam;
    public Team npcTeam;
    public Team weatherTeam;

    public void LoadContext(Team allyTeam, Team foeTeam)
    {
        this.allyTeam = allyTeam;
        this.foeTeam = foeTeam;
    }

    public void ShowPickers()
    {
        foreach (var battler in allyTeam.battlers)
        {
            battler.ShowPicker(battler.isAlive);
        }
    }

    public void ShowTargets(Battler user, TargetData target)
    {
        switch (target.type)
        {
            case TargetType.Self:
                ShowSingleTarget(user, target);
                break;
            case TargetType.Single:
                if (target.TargetAllies)
                {
                    foreach (var ally in allyTeam.battlers)
                    {
                        ShowSingleTarget(ally, target);
                    }
                }
                if (target.TargetFoes)
                {
                    foreach (var foe in foeTeam.battlers)
                    {
                        ShowSingleTarget(foe, target);
                    }
                }
                break;
            case TargetType.Team:
                if (target.TargetAllies)
                {
                    allyTeam.ShowTarget(true);
                }
                if (target.TargetFoes)
                {
                    foeTeam.ShowTarget(true);
                }
                break;
            case TargetType.Mixed:
                if (target.TargetAllies)
                {
                    allyTeam.ShowTarget(true);
                    foreach (var ally in allyTeam.battlers)
                    {
                        ShowSingleTarget(ally, target);
                    }
                }
                if (target.TargetFoes)
                {
                    foeTeam.ShowTarget(true);
                    foreach (var foe in foeTeam.battlers)
                    {
                        ShowSingleTarget(foe, target);
                    }
                }
                break;
            case TargetType.All:
            default:
                weatherTeam.ShowTarget(true);
                break;
        }
    }

    private void ShowSingleTarget(Battler battler, TargetData data)
    {
        if ((battler.isAlive && data.TargetAlive) || (!battler.isAlive && data.TargetDead))
        {
            battler.ShowTarget(true);
        }
    }

    public void HidePickers()
    {
        foreach (var battler in playerTeam.battlers)
        {
            battler.ShowPicker(false);
        }
        foreach (var battler in npcTeam.battlers)
        {
            battler.ShowPicker(false);
        }
    }

    public void HideTargets()
    {
        playerTeam.ShowTarget(false);
        npcTeam.ShowTarget(false);
        weatherTeam.ShowTarget(false);

        foreach (var battler in playerTeam.battlers)
        {
            battler.ShowTarget(false);
        }
        foreach (var battler in npcTeam.battlers)
        {
            battler.ShowTarget(false);
        }
    }
}
