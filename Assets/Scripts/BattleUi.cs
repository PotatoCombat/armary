using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleUi
{
    [Header("Infos")]
    public WaveInfo wave;
    public WeatherInfo weather;
    public List<HealthBar> players;
    public List<HealthBar> npcs;
}
