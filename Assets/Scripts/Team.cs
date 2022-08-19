using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

public class Team : MonoBehaviour
{
    public Faction faction;
    public List<Battler> battlers;

    // Equips
    // Items

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
        foreach (var battler in battlers)
        {
            battler.PlayAnimation("Intro");
        }
    }

    public void Hide()
    {
        foreach (var battler in battlers)
        {
            battler.PlayAnimation("Flee");
        }
    }

    private void LoadBattlers(List<BattlerData> battlerDatas)
    {
        for (var i = 0; i < battlerDatas.Count; i++)
        {
            if (i < battlers.Count)
            {
                battlers[i].Load(battlerDatas[i]);
            }
            else
            {
                battlers[i].Hide();
            }
        }
    }

    private void LoadBattlers(List<BattlerTemplate> battlerTemplates)
    {
        for (var i = 0; i < battlerTemplates.Count; i++)
        {
            if (i < battlers.Count)
            {
                battlers[i].Load(battlerTemplates[i].CreateBattlerData());
            }
            else
            {
                battlers[i].Hide();
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

#if UNITY_EDITOR
    [ContextMenu("Fetch Battlers")]
    private void FetchBattlers()
    {
        battlers = GetComponentsInChildren<Battler>(true).ToList();
        foreach (var battler in battlers)
        {
            battler.faction = faction;
            EditorUtility.SetDirty(battler);
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
