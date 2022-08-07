using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleData : MonoBehaviour
{
    public List<BattlerData> allies;
    public List<BattlerData> foes;

    public FormationType foeFormation;

    public List<BattlerType> allyTypes;
    public List<BattlerType> foeTypes;

    private void Start()
    {
        allies = allyTypes.Select(type => type.CreateInstance()).ToList();
        foes = foeTypes.Select(type => type.CreateInstance()).ToList();
    }
}
