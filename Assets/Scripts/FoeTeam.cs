using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoeTeam : MonoBehaviour
{
    public List<BattleTeam> waves;

    private void Reset()
    {
        waves = GetComponentsInChildren<BattleTeam>(true).ToList();
    }
}
