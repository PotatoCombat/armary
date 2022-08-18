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

    [Header("Entities")]
    public Battler weather;
    public List<Battler> players;
    public List<Battler> npcs;

    [Header("Targets")]
    [SerializeField] private TargetGroup targetAll;
    [SerializeField] private TargetGroup targetPlayers;
    [SerializeField] private TargetGroup targetNpcs;

    [Space]
    public UnityEvent<Battler> onPickBattler;
    public UnityEvent<List<Battler>> onTargetBattlers;

    private List<Battler> _battlers;
    public List<Battler> Battlers {
        get
        {
            if (_battlers == null)
            {
                _battlers = new List<Battler>();
                _battlers.AddRange(players);
                _battlers.AddRange(npcs);
            }
            return _battlers;
        }
    }

    // public void LoadPlayers(List<BattlerData> battlers)
    // {
    //     for (var i = 0; i < players.Count; i++)
    //     {
    //         if (i < battlers.Count)
    //         {
    //             // players[i].transform.localPosition = positions.GetAllyPosition(allies.Count, i);
    //             players[i].Load(battlers[i]);
    //             players[i].PlayAnimation("Intro");
    //         }
    //         else
    //         {
    //             players[i].Hide();
    //         }
    //     }
    // }
    //
    // public void LoadNpcs(List<BattlerTemplate> templates)
    // {
    //     for (var i = 0; i < npcs.Count; i++)
    //     {
    //         if (i < templates.Count)
    //         {
    //             npcs[i].transform.localPosition = templates[i].position;
    //             npcs[i].Load(templates[i].CreateBattlerData());
    //             npcs[i].PlayDelayedAnimation("Intro", 0);
    //         }
    //         else
    //         {
    //             npcs[i].Hide();
    //         }
    //     }
    // }

    public void ShowPickers(Faction faction)
    {
        var allies = faction == Faction.Player ? players : npcs;
        foreach (var ally in allies)
        {
            ShowSinglePicker(ally);
        }
    }

    private void ShowSinglePicker(Battler battler)
    {
        if (battler.isAlive)
        {
            battler.picker.gameObject.SetActive(true);
        }
    }

    public void ShowTargets(Battler user, TargetData data)
    {
        var allies = user.faction == Faction.Player ? players : npcs;
        var foes = user.faction == Faction.Player ? npcs : players;

        var targetAllies = user.faction == Faction.Player ? targetPlayers : targetNpcs;
        var targetFoes = user.faction == Faction.Player ? targetNpcs : targetPlayers;

        switch (data.type)
        {
            case TargetData.Type.Self:
                ShowSingleTarget(user, data);
                break;
            case TargetData.Type.Single:
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
            case TargetData.Type.Team:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(targetAllies);
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(targetFoes);
                }
                break;
            case TargetData.Type.Mixed:
                if (data.TargetAllies)
                {
                    ShowGroupTarget(targetAllies);
                    for (var i = 0; i < allies.Count; i++)
                    {
                        ShowSingleTarget(allies[i], data);
                    }
                }
                if (data.TargetFoes)
                {
                    ShowGroupTarget(targetFoes);
                    for (var i = 0; i < foes.Count; i++)
                    {
                        ShowSingleTarget(foes[i], data);
                    }
                }
                break;
            case TargetData.Type.All:
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

    public void HideTargets()
    {
        targetAll.target.gameObject.SetActive(false);
        targetPlayers.target.gameObject.SetActive(false);
        targetNpcs.target.gameObject.SetActive(false);

        foreach (var battler in players)
        {
            battler.target.gameObject.SetActive(false);
        }
        foreach (var battler in npcs)
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
        onTargetBattlers.Invoke(players);
    }

    public void TargetNpcs()
    {
        onTargetBattlers.Invoke(npcs);
    }

    public void TargetAll()
    {
        onTargetBattlers.Invoke(Battlers);
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
