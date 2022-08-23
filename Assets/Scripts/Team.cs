using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Team : MonoBehaviour
{
    public Faction faction;
    public SimpleButton target;

    public List<Battler> battlers;

    public Events events;

    [Serializable]
    public class Events
    {
        public UnityEvent<Team> onTarget;
    }

    // Equips
    // Items

    public void ShowTarget(bool visible)
    {
        target.gameObject.SetActive(visible);
    }

    public void Load(PartyData data)
    {
        LoadBattlers(data.allies);
        // LoadEquipment
        // LoadItems
    }

    public void Load(WaveData wave)
    {
        LoadBattlers(wave.foes);
        // LoadEquipment
        // LoadItems
    }

    public void Show()
    {
        // foreach (var battler in battlers)
        // {
        //     battler.PlayAnimation("Intro");
        // }
    }

    public void Hide()
    {
        // foreach (var battler in battlers)
        // {
        //     battler.PlayAnimation("Flee");
        // }
    }

    private void LoadBattlers(List<BattlerData> battlerDatas)
    {
        var numBattlers = battlerDatas.Count;
        for (var i = 0; i < battlers.Count; i++)
        {
            if (i < numBattlers)
            {
                battlers[i].LoadData(battlerDatas[i]);
                battlers[i].gameObject.SetActive(true);
            }
            else
            {
                battlers[i].gameObject.SetActive(false);
            }
        }
    }

    private void LoadBattlers(List<BattlerTemplate> battlerTemplates)
    {
        var numBattlers = battlerTemplates.Count;
        for (var i = 0; i < battlers.Count; i++)
        {
            if (i < numBattlers)
            {
                battlers[i].LoadData(battlerTemplates[i].CreateBattlerData());
                battlers[i].gameObject.SetActive(true);
            }
            else
            {
                battlers[i].gameObject.SetActive(false);
            }
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

    public void Target()
    {
        events.onTarget.Invoke(this);
    }
}
