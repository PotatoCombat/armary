using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BattleStage : MonoBehaviour
{
    [Serializable]
    private class TargetGroup
    {
        public HoverButton target;
        public string description;
    }

    [Header("Teams")]
    public Team playerTeam;
    public Team npcTeam;

    [Header("Targets")]
    [SerializeField] private TargetGroup targetAll;
    [SerializeField] private TargetGroup targetPlayers;
    [SerializeField] private TargetGroup targetNpcs;

    [Space]
    public UnityEvent<Battler> onPickBattler;
    public UnityEvent<List<Battler>> onTargetBattlers;

    public void ShowPickers(Battler user, Team team)
    {
        foreach (var battler in team.battlers)
        {
            battler.picker.gameObject.SetActive(battler.isAlive && battler != user);
        }
    }

    public void ShowTargets(Battler user, TargetData data)
    {
        var allies = user.faction == Faction.Player ? playerTeam.battlers : npcTeam.battlers;
        var foes = user.faction == Faction.Player ? npcTeam.battlers : playerTeam.battlers;

        var targetAllies = user.faction == Faction.Player ? targetPlayers : targetNpcs;
        var targetFoes = user.faction == Faction.Player ? targetNpcs : targetPlayers;

        switch (data.type)
        {
            case TargetType.Self:
                ShowSingleTarget(user, data);
                break;
            case TargetType.Single:
                if (data.TargetAllies)
                {
                    foreach (var ally in allies)
                    {
                        ShowSingleTarget(ally, data);
                    }
                }
                if (data.TargetFoes)
                {
                    foreach (var foe in foes)
                    {
                        ShowSingleTarget(foe, data);
                    }
                }
                break;
            case TargetType.Team:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(targetAllies);
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(targetFoes);
                }
                break;
            case TargetType.Mixed:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(targetAllies);
                    foreach (var ally in allies)
                    {
                        ShowSingleTarget(ally, data);
                    }
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(targetFoes);
                    foreach (var foe in foes)
                    {
                        ShowSingleTarget(foe, data);
                    }
                }
                break;
            case TargetType.All:
            default:
                ShowGroupTarget(targetAll);
                break;
        }
    }

    private void ShowSingleTarget(Battler battler, TargetData data)
    {
        if ((battler.isAlive && data.TargetAlive)
            || (!battler.isAlive && data.TargetDead))
        {
            battler.target.gameObject.SetActive(true);
        }
    }

    private void ShowGroupTarget(TargetGroup targetGroup)
    {
        targetGroup.target.gameObject.SetActive(true);
    }

    public void HidePickers()
    {
        foreach (var battler in playerTeam.battlers)
        {
            battler.picker.gameObject.SetActive(false);
        }
        foreach (var battler in npcTeam.battlers)
        {
            battler.picker.gameObject.SetActive(false);
        }
    }

    public void HideTargets()
    {
        targetAll.target.gameObject.SetActive(false);
        targetPlayers.target.gameObject.SetActive(false);
        targetNpcs.target.gameObject.SetActive(false);

        foreach (var battler in playerTeam.battlers)
        {
            battler.target.gameObject.SetActive(false);
        }
        foreach (var battler in npcTeam.battlers)
        {
            battler.target.gameObject.SetActive(false);
        }
    }

    public void PickBattler(Battler battler)
    {
        onPickBattler.Invoke(battler);
    }

    public void TargetBattler(Battler battler)
    {
        onTargetBattlers.Invoke(new List<Battler>(){battler});
    }

    public void TargetPlayers()
    {
        onTargetBattlers.Invoke(playerTeam.battlers);
    }

    public void TargetNpcs()
    {
        onTargetBattlers.Invoke(npcTeam.battlers);
    }

    public void TargetAll()
    {
        var battlers = new List<Battler>();
        battlers.AddRange(playerTeam.battlers);
        battlers.AddRange(npcTeam.battlers);
        onTargetBattlers.Invoke(battlers);
    }

    // public void ShowPlayerTooltip(int i, Vector2 position)
    // {
    //     Debug.Log(players.battlers[i].name);
    // }
    //
    // public void ShowNpcTooltip(int i, Vector2 position)
    // {
    //     Debug.Log(npcs.battlers[i].name);
    // }
    //
    // public void ShowPlayersTooltip()
    // {
    //     Debug.Log(targetPlayers.description);
    // }
    //
    // public void ShowNpcsTooltip()
    // {
    //     Debug.Log(targetNpcs.description);
    // }
    //
    // public void ShowAllTooltip()
    // {
    //     Debug.Log(targetAll.description);
    // }

    // public void HideTooltip()
    // {
    //
    // }
}
