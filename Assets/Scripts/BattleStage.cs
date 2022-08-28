using System;
using UnityEngine;

[Serializable]
public class BattleStage : MonoBehaviour
{
    [SerializeField] private BattleContext context;

    public void ShowPickers()
    {
        foreach (var battler in context.AllyTeam.battlers)
        {
            battler.ShowPicker(battler.IsAlive);
        }
        context.User.ShowPicker(false);
    }

    public void ShowTargets()
    {
        var user = context.User;
        var target = context.Move.target;

        switch (context.Move.target.type)
        {
            case TargetType.Self:
                ShowSingleTarget(user, target);
                break;
            case TargetType.Single:
                if (target.TargetAllies)
                {
                    foreach (var ally in context.AllyTeam.battlers)
                    {
                        ShowSingleTarget(ally, target);
                    }
                }
                if (target.TargetFoes)
                {
                    foreach (var foe in context.FoeTeam.battlers)
                    {
                        ShowSingleTarget(foe, target);
                    }
                }
                break;
            case TargetType.Team:
                if (target.TargetAllies)
                {
                    context.AllyTeam.ShowTarget(true);
                }
                if (target.TargetFoes)
                {
                    context.FoeTeam.ShowTarget(true);
                }
                break;
            case TargetType.Mixed:
                if (target.TargetAllies)
                {
                    context.AllyTeam.ShowTarget(true);
                    foreach (var ally in context.AllyTeam.battlers)
                    {
                        ShowSingleTarget(ally, target);
                    }
                }
                if (target.TargetFoes)
                {
                    context.FoeTeam.ShowTarget(true);
                    foreach (var foe in context.FoeTeam.battlers)
                    {
                        ShowSingleTarget(foe, target);
                    }
                }
                break;
            case TargetType.All:
            default:
                context.WeatherTeam.ShowTarget(true);
                break;
        }
    }

    private void ShowSingleTarget(Battler battler, TargetData data)
    {
        if ((battler.IsAlive && data.TargetAlive) || (!battler.IsAlive && data.TargetDead))
        {
            battler.ShowTarget(true);
        }
    }

    public void HidePickers()
    {
        foreach (var battler in context.AllyTeam.battlers)
        {
            battler.ShowPicker(false);
        }
        foreach (var battler in context.NpcTeam.battlers)
        {
            battler.ShowPicker(false);
        }
    }

    public void HideTargets()
    {
        context.AllyTeam.ShowTarget(false);
        context.FoeTeam.ShowTarget(false);
        context.WeatherTeam.ShowTarget(false);

        foreach (var battler in context.AllyTeam.battlers)
        {
            battler.ShowTarget(false);
        }
        foreach (var battler in context.FoeTeam.battlers)
        {
            battler.ShowTarget(false);
        }
    }
}
