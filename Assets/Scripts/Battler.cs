using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    public Faction faction;
    public bool isAlive;
    public int actions = 0;

    public Actor model;
    public Actor effects;

    public TextMeshProUGUI hpText;

    public HoverButton picker;
    public HoverButton target;

    public BattlerData data;
    public Events events;

    [Serializable]
    public class Events
    {
        public UnityEvent<Battler> onSelect;
        public UnityEvent<Battler> onTarget;
    }

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

    public void Load(BattlerData data)
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
}
