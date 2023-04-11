using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image cpBar;

    public Sprite Icon
    {
        get => icon.sprite;
        set => icon.sprite = value;
    }

    public void ShowHp(int hp, int maxHp)
    {
        hpText.text = $"{hp} / {maxHp}";
        hpBar.fillAmount = (float)hp / maxHp;
    }
}
