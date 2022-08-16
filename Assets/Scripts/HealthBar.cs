using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battlerName;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI maxHp;
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private RectTransform cpBar;

    private int _hpValue = 100;
    private int _maxHpValue = 100;
    private int _cpValue = 100;

    private void Start()
    {
        UpdateHp();
        UpdateCp();
    }

    public string BattlerName
    {
        get => battlerName.text;
        set => battlerName.text = value;
    }

    public int Hp
    {
        get => _hpValue;
        set
        {
            _hpValue = value;
            UpdateHp();
        }
    }

    public int MaxHp
    {
        get => _maxHpValue;
        set
        {
            _maxHpValue = value;
            UpdateHp();
        }
    }

    public int Cp
    {
        get => _cpValue;
        set
        {
            _cpValue = value;
            UpdateCp();
        }
    }

    private void UpdateHp()
    {
        hp.text = _hpValue.ToString();
        maxHp.text = _maxHpValue.ToString();
        hpBar.anchorMax = new Vector2(_hpValue / (float)_maxHpValue, 1f);
    }

    private void UpdateCp()
    {
        cpBar.anchorMax = new Vector2(_hpValue / 100f, 1f);
    }
}
