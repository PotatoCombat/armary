using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    public Faction faction;
    public int actions = 0;
    // TODO: Bool flag to indicate hit foes / foes that can counter
    // public bool hit = false;

    public Actor model;
    public Actor effects;

    public TextMeshProUGUI hpText;

    public SimpleButton picker;
    public SimpleButton target;

    public BattlerData data;
    public Events events;

    [Serializable]
    public class Events
    {
        public UnityEvent<Battler> onSelect;
        public UnityEvent<Battler> onTarget;
        // public UnityEvent<Battler, HitEvent> onHit;
    }

    // private void OnEnable()
    // {
    //     hitReceiver.onHit.AddListener(() => onHit.Raise(this));
    // }

    public bool IsAlive => data.hp > 0;

    public void ShowPicker(bool visible)
    {
        picker.gameObject.SetActive(visible);
    }

    public void ShowTarget(bool visible)
    {
        target.gameObject.SetActive(visible);
    }

    public void ShowTooltip(bool visible)
    {

    }

    public void LoadData(BattlerData data)
    {
        // Model and AI
        this.data = data;
        gameObject.SetActive(true);
    }

    public void Select()
    {
        events.onSelect.Invoke(this);
    }

    public void Target()
    {
        events.onTarget.Invoke(this);
    }

    // public void Hit(HitData hitData)
    // {
        // onHit.Raise(this, hitData);
        // events.onHit.Invoke(this, hitEvent);
    // }
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
