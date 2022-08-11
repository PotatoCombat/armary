using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleTeam : MonoBehaviour
{
    public List<Battler> battlers;

    private void Reset()
    {
        battlers = GetComponentsInChildren<Battler>(true).ToList();
    }
}
