using System;
using UnityEngine;

[Serializable]
public class BattleStage : MonoBehaviour
{
    [Header("Teams")]
    public Team playerTeam;
    public Team npcTeam;
    public Team weatherTeam;

    public Team allyTeam;
    public Team foeTeam;

    public void ShowPickers(Team team)
    {
        foreach (var battler in team.battlers)
        {
            battler.ShowPicker(battler.isAlive);
        }
    }

    public void ShowTargets(Battler user, TargetData data)
    {
        switch (data.type)
        {
            case TargetType.Self:
                ShowSingleTarget(user, data);
                break;
            case TargetType.Single:
                if (data.TargetAllies)
                {
                    foreach (var ally in allyTeam.battlers)
                    {
                        ShowSingleTarget(ally, data);
                    }
                }
                if (data.TargetFoes)
                {
                    foreach (var foe in foeTeam.battlers)
                    {
                        ShowSingleTarget(foe, data);
                    }
                }
                break;
            case TargetType.Team:
                if (data.TargetAllies)
                {
                    allyTeam.ShowTarget(true);
                }
                if (data.TargetFoes)
                {
                    foeTeam.ShowTarget(true);
                }
                break;
            case TargetType.Mixed:
                if (data.TargetAllies)
                {
                    allyTeam.ShowTarget(true);
                    foreach (var ally in allyTeam.battlers)
                    {
                        ShowSingleTarget(ally, data);
                    }
                }
                if (data.TargetFoes)
                {
                    foeTeam.ShowTarget(true);
                    foreach (var foe in foeTeam.battlers)
                    {
                        ShowSingleTarget(foe, data);
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
