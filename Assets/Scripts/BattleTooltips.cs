using UnityEngine;

public class BattleTooltips : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MoveTooltip moveTooltip;

    [Header("Events")]
    [SerializeField] private BattleEvent onHideTooltips;
    [SerializeField] private MoveTypeEvent onMoveTooltip;

    private void OnEnable()
    {
        onHideTooltips.AddListener(HideAll);
        onMoveTooltip.AddListener(Show);
    }

    private void OnDisable()
    {
        onHideTooltips.RemoveListener(HideAll);
        onMoveTooltip.RemoveListener(Show);
    }

    public void HideAll()
    {
        moveTooltip.gameObject.SetActive(false);
    }

    public void Show(MoveType move)
    {
        moveTooltip.Show(move);
        moveTooltip.gameObject.SetActive(true);
    }
}
