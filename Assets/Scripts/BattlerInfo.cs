using TMPro;
using UnityEngine;

public class BattlerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battlerName;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private RectTransform cpBar;

    public void SetBattlerName(string battlerName)
    {
        this.battlerName.text = $"{battlerName}";
    }

    public void SetHp(int hp, int maxHp)
    {
        this.hp.text = $"{hp}/{maxHp}";
    }
}
