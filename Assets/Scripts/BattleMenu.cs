using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public bool canFlee;
    public Battler user;
    public Team team;
    public BattleMenuPanel currentPanel;

    // public Equipment equipments;
    // public Equipment items;

    [Header("Panels")]
    public Category history;
    public Category attacks;
    public Category tactics;
    public Category skills;
    public Category equips;
    public Category equipsExpanded;
    public Category items;

    [Header("Tooltips")]
    public MoveTooltip moveTooltip;

    [Space]
    public UnityEvent<MoveData> onSelectMove;

    [Serializable]
    public class Category
    {
        public SimpleButton button;
        public BattleMenuPanel panel;
    }

    public void Show(Battler user, Team team)
    {
        this.user = user;
        this.team = team;
        gameObject.SetActive(true);
        if (currentPanel != null)
        {
            currentPanel.Show(user, team);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentPanel.Hide();
    }

    public void ShowHistory()
    {
        ShowCategory(history);
    }

    public void ShowAttacks()
    {
        if (user.data.moves.Length <= 0)
        {
            SelectMove(user.data.attack);
        }
        else
        {
            ShowCategory(attacks);
        }
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
        ShowCategory(tactics);
        // ShowPanel(menu.tactics);
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
        ShowCategory(skills);
        // ShowPanel(menu.skills);
        /*
         *      options.Show(user.skills);
         */
    }

    public void ShowEquips()
    {
        ShowCategory(equips);
        /*
         *      options.Show(user.equips);
         */
    }

    public void ShowItems()
    {
        ShowCategory(items);
        /*
         *      options.Show(user.items);
         */
    }

    private void ShowCategory(Category category)
    {
        if (currentPanel)
        {
            currentPanel.Hide();
        }
        currentPanel = category.panel;
        currentPanel.Show(user, team);
    }

    public void SelectMove(MoveData move)
    {
        onSelectMove.Invoke(move);
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
