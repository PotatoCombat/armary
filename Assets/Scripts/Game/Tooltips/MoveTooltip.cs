using TMPro;
using UnityEngine;

public class MoveTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveName;

    public void Show(MoveType move)
    {
        moveName.text = move.name;
    }
}
