using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    public HoverButton attack;
    public HoverButton tactic;
    public HoverButton skill;
    public HoverButton equip;
    public HoverButton item;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
