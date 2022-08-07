using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Formations : MonoBehaviour
{
    [Header("Allies")]
    [SerializeField] private Formation allies1;
    [SerializeField] private Formation allies2;
    [SerializeField] private Formation allies3;

    private Dictionary<int, Formation> _allyFormations;
    private Dictionary<FormationType, Formation> _foeFormations;

    private void Start()
    {
        LoadAllyFormations();
        LoadFoeFormations();
    }

    [ContextMenu("Load Children")]
    private void LoadChildren()
    {
        foreach (var formation in GetComponentsInChildren<Formation>(true))
        {
            formation.Load();
        }
    }

    private void LoadAllyFormations()
    {
        _allyFormations = new Dictionary<int, Formation>
        {
            [1] = allies1,
            [2] = allies2,
            [3] = allies3,
        };
    }

    private void LoadFoeFormations()
    {
        _foeFormations = new Dictionary<FormationType, Formation>();
        foreach (var formation in GetComponentsInChildren<Formation>(true))
        {
            if (formation.type)
            {
                _foeFormations[formation.type] = formation;
            }
        }
    }

    public Formation GetAllyFormation(int allyCount)
    {
        return _allyFormations.TryGetValue(allyCount, out var formation) ? formation : null;
    }

    public Formation GetFoeFormation(FormationType formationType)
    {
        return _foeFormations.TryGetValue(formationType, out var formation) ? formation : null;
    }
}
