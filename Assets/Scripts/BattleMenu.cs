using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    [Header("Categories")]
    public HoverButton attack;
    public HoverButton tactic;
    public HoverButton skill;
    public HoverButton equip;
    public HoverButton item;

    [Header("Panels")]
    public ButtonGroup attacks;
    public ButtonGroup tactics;
    public ButtonGroup skills;
    public ButtonGroup equips;
    public ButtonGroup expandedEquips;
    public ButtonGroup items;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
