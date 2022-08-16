using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleUi
{
    public BattleMenu menu;
    public ButtonGroup lastPanel;

    [Header("Panels")]
    public ButtonGroup attacks;
    public ButtonGroup tactics;
    public ButtonGroup skills;
    public ButtonGroup equips;
    public ButtonGroup expandedEquips;
    public ButtonGroup items;

    [Header("Tooltips")]
    public MoveTooltip moveTooltip;

    [Header("Infos")]
    public List<HealthBar> players;
    public List<HealthBar> npcs;
    public WeatherInfo weather;
    public WaveInfo wave;
}
