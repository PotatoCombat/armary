using TMPro;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [Header("Runtime")]
    public Faction faction;
    //  TODO: public bool hit = false; // to mark which foes can counter
    public int actions = 0;
    public BattlerData data; // TODO: make private

    [Header("Components")]
    public Actor model;
    public Actor effects;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private SimpleButton picker;
    [SerializeField] private SimpleButton target;

    public bool IsAlive => data.hp > 0;
    public bool IsControllable => faction == Faction.Player;

    public void LoadData(BattlerData data)
    {
        // Model and AI
        this.data = data;
        gameObject.SetActive(true);
    }

    public void ShowPicker(bool visible)
    {
        picker.gameObject.SetActive(visible);
    }

    public void ShowTarget(bool visible)
    {
        target.gameObject.SetActive(visible);
    }

    public BattleDecision GetTurnDecision(BattleContext context)
    {
        return data.type.ai.GetTurnDecision(context);
    }

    public BattleDecision GetCounterDecision(BattleContext context)
    {
        return data.type.ai.GetCounterDecision(context);
    }

    public void Damage(int value)
    {
        Hit(-value);
    }

    public void Heal(int value)
    {
        Hit(value);
    }

    private void Hit(int value)
    {
        // hpText.text = damage.value.ToString();
        var prevHp = data.hp;
        data.hp = Mathf.Clamp(data.hp + value, 0, data.maxHp);
        if (data.hp == prevHp)
        {
            return;
        }
        if (data.hp == 0)
        {
            model.Animate("Die");
        }
        else if (prevHp == 0)
        {
            model.Animate("Revive");
        }
        else if (data.hp < prevHp)
        {
            model.Animate("Hit");
        }
        else if (data.hp > prevHp)
        {
            model.Animate("Heal");
        }
    }
}
