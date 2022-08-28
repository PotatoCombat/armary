using System.Collections.Generic;
using UnityEngine;

public abstract class MoveLogic : ScriptableObject
{
    public abstract List<HitCommand> CreateHits(BattleManager manager);
}
