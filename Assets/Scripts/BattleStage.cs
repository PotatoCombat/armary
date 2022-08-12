using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleStage
{
    public Transform transform;
    public List<Battler> players;
    public List<Battler> npcs;
    public Battler weather;

    public void LoadPlayers(List<BattlerData> battlers)
    {
        for (var i = 0; i < players.Count; i++)
        {
            if (i < battlers.Count)
            {
                // players[i].transform.localPosition = positions.GetAllyPosition(allies.Count, i);
                players[i].Load(battlers[i]);
                players[i].PlayAnimation("Intro");
            }
            else
            {
                players[i].Hide();
            }
        }
    }

    public void LoadNpcs(List<BattlerTemplate> templates)
    {
        for (var i = 0; i < npcs.Count; i++)
        {
            if (i < templates.Count)
            {
                npcs[i].transform.localPosition = templates[i].position;
                npcs[i].Load(templates[i].CreateBattlerData());
                npcs[i].PlayDelayedAnimation("Intro", 0);
            }
            else
            {
                npcs[i].Hide();
            }
        }
    }
}
