using TMPro;
using UnityEngine;

public class MoveTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveName;

    public string MoveName
    {
        get => moveName.text;
        set => moveName.text = value;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
