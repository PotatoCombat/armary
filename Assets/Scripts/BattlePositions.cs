using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattlePositions
{
    [SerializeField] private List<Transform> allies1;
    [SerializeField] private List<Transform> allies2;
    [SerializeField] private List<Transform> allies3;

    [SerializeField] private Transform allyTeam;
    [SerializeField] private Transform foeTeam;

    public Vector3 GetAllyPosition(int numAllies, int index)
    {
        var transforms = numAllies switch
        {
            1 => allies1,
            2 => allies2,
            3 => allies3,
            _ => new List<Transform>()
        };
        return transforms[index].localPosition;
    }

    public Vector3 GetAllyTeamPosition => allyTeam.localPosition;
    public Vector3 GetFoeTeamPosition => foeTeam.localPosition;
}
