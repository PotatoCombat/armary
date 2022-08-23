using TMPro;
using UnityEngine;

public class MoveTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveName;

    public void LoadData(MoveData move)
    {
        moveName.text = move.name;
    }
}
