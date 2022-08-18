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

    [Header("Menu")]
    public BattleMenu menu;
    public ButtonGroup lastPanel;

    [Header("Targets")]
    public BattleTargets targets;

    [Header("Tooltips")]
    public MoveTooltip moveTooltip;

    public void HidePanel()
    {
        lastPanel.Hide();
        lastPanel = null;
    }

    private void ShowPanel(ButtonGroup panel, Action<HoverButton, int> setButton = null)
    {
        if (lastPanel)
        {
            lastPanel.Hide();
        }
        lastPanel = panel;
        panel.Show(setButton);
    }

    public void ShowAttacks()
    {
        ShowPanel(menu.attacks); // (button, i) => button.targetImage.sprite = user.data.moves[i].sprite);
        /*
         * if user.limitbreak
         *      options.Show(attack + user.limitbreak skills);
         * else
         *      SelectMove(user.attack);
         */
        // options.gameObject.SetActive(true);
    }

    public void ShowTactics()
    {
        ShowPanel(menu.tactics);
        /*
         * if allies.Count > 3
         *      options.Show(defend + flee, swap1);
         * if allies.Count > 3
         *      options.Show(defend + flee, swap1, swap2);
         * else
         *      SelectMove(defend + flee);
         */
    }

    public void ShowSkills()
    {
        ShowPanel(menu.skills);
        /*
         *      options.Show(user.skills);
         */
    }

    public void ShowEquips()
    {
        ShowPanel(menu.equips);
        /*
         *      options.Show(user.equips);
         */
    }

    public void ShowItems()
    {
        ShowPanel(menu.items);
        /*
         *      options.Show(user.items);
         */
    }

    public void ShowMoveTooltip(int index, Vector2 position)
    {
        moveTooltip.Show();
    }

    public void HideMoveTooltip()
    {
        moveTooltip.Hide();
    }
}
