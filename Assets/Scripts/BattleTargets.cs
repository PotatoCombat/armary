using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTargets : MonoBehaviour
{
    [Serializable]
    private class BattleTeam
    {
        public HoverButton teamTarget;
        public List<HoverButton> singleTargets;
        public List<Battler> battlers;
    }

    [Header("Targets")]
    [SerializeField] private HoverButton allTarget;
    [SerializeField] private BattleTeam players;
    [SerializeField] private BattleTeam npcs;

    [Header("Tooltips")]
    public string targetPlayersText = "Target All Players";
    public string targetNpcsText = "Target All Npcs";
    public string targetAllText = "Target All";

    [Space]
    public UnityEvent<List<Battler>> onSelectTarget;

    public void Show(TargetData data, Battler user)
    {
        var allyTeam = user.faction == Faction.Player ? players : npcs;
        var foeTeam = user.faction == Faction.Player ? npcs : players;

        switch (data.type)
        {
            case TargetData.Type.Self:
                for (var i = 0; i < allyTeam.battlers.Count; i++)
                {
                    if (allyTeam.battlers[i] == user)
                    {
                        ShowSingleTarget(data, user, allyTeam.singleTargets[i]);
                        break;
                    }
                }
                break;
            case TargetData.Type.Single:
                if (data.TargetAllies)
                {
                    for (var i = 0; i < allyTeam.battlers.Count; i++)
                    {
                        ShowSingleTarget(data, allyTeam.battlers[i], allyTeam.singleTargets[i]);
                    }
                }
                if (data.TargetFoes)
                {
                    for (var i = 0; i < foeTeam.battlers.Count; i++)
                    {
                        ShowSingleTarget(data, foeTeam.battlers[i], foeTeam.singleTargets[i]);
                    }
                }
                break;
            case TargetData.Type.Team:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(allyTeam.teamTarget);
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(foeTeam.teamTarget);
                }
                break;
            case TargetData.Type.Mixed:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(allyTeam.teamTarget);
                    for (var i = 0; i < allyTeam.battlers.Count; i++)
                    {
                        ShowSingleTarget(data, allyTeam.battlers[i], allyTeam.singleTargets[i]);
                    }
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(foeTeam.teamTarget);
                    for (var i = 0; i < foeTeam.battlers.Count; i++)
                    {
                        ShowSingleTarget(data, foeTeam.battlers[i], foeTeam.singleTargets[i]);
                    }
                }
                break;
            case TargetData.Type.All:
            default:
                ShowGroupTarget(allTarget);
                break;
        }
    }

    private void ShowSingleTarget(TargetData data, Battler battler, HoverButton target)
    {
        if ((battler.IsAlive && data.TargetAlive)
            || (!battler.IsAlive && data.TargetDead))
        {
            target.transform.position = battler.transform.position;
            target.gameObject.SetActive(true);
        }
    }

    private void ShowGroupTarget(HoverButton target)
    {
        target.gameObject.SetActive(true);
    }

    public void Hide()
    {
        allTarget.gameObject.SetActive(false);
        players.teamTarget.gameObject.SetActive(false);
        npcs.teamTarget.gameObject.SetActive(false);

        foreach (var target in players.singleTargets)
        {
            target.gameObject.SetActive(false);
        }
        foreach (var target in npcs.singleTargets)
        {
            target.gameObject.SetActive(false);
        }
    }

    public void TargetPlayer(int i)
    {
        onSelectTarget.Invoke(new List<Battler>{players.battlers[i]});
    }

    public void TargetNpc(int i)
    {
        onSelectTarget.Invoke(new List<Battler>{npcs.battlers[i]});
    }

    public void TargetPlayers()
    {
        onSelectTarget.Invoke(players.battlers);
    }

    public void TargetNpcs()
    {
        onSelectTarget.Invoke(npcs.battlers);
    }

    public void TargetAll()
    {
        var battlers = new List<Battler>();
        battlers.AddRange(players.battlers);
        battlers.AddRange(npcs.battlers);
        onSelectTarget.Invoke(battlers);
    }

    public void ShowPlayerTooltip(int i, Vector2 position)
    {
        Debug.Log(players.battlers[i].name);
    }

    public void ShowNpcTooltip(int i, Vector2 position)
    {
        Debug.Log(npcs.battlers[i].name);
    }

    public void ShowPlayersTooltip()
    {
        Debug.Log(targetPlayersText);
    }

    public void ShowNpcsTooltip()
    {
        Debug.Log(targetNpcsText);
    }

    public void ShowAllTooltip()
    {
        Debug.Log(targetAllText);
    }

    public void HideTooltip()
    {

    }
}
