using UnityEngine;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public Battler user;
    public Team team;
    public BattleMenuPanel panel;

    [Header("Components")]
    public GameObject cancelButton;
    public BattleMenuPanel listPanel;
    public BattleMenuPanel defaultPanel;
    public MoveTooltip moveTooltip;

    [Header("Events")]
    public UnityEvent<MoveType> onSelectMove;
    public UnityEvent onCancelMove;

    public void LoadContext(Battler user, Team team)
    {
        this.user = user;
        this.team = team;

        listPanel.LoadContext(user, team);
        if (panel)
        {
            panel.LoadContext(user, team);
        }
    }

    public void Show()
    {
        ShowInterface(true);
        ShowCancelButton(false);
    }

    public void Hide()
    {
        ShowInterface(false);
        ShowCancelButton(false);
    }

    public void ShowTooltip(MoveType move)
    {
        moveTooltip.LoadData(move);
        moveTooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        moveTooltip.gameObject.SetActive(false);
    }

    public void SelectDefaultPanel()
    {
        SelectPanel(defaultPanel);
    }

    public void SelectPanel(BattleMenuPanel panel)
    {
        if (this.panel)
        {
            this.panel.gameObject.SetActive(false);
        }
        this.panel = panel;
        this.panel.LoadContext(user, team);
        this.panel.gameObject.SetActive(true);
    }

    public void SelectMove(MoveType move)
    {
        onSelectMove.Invoke(move);
        ShowInterface(false);
        ShowCancelButton(true);
        HideTooltip();
    }

    public void CancelMove()
    {
        onCancelMove.Invoke();
        ShowInterface(true);
        ShowCancelButton(false);
    }

    private void ShowInterface(bool visible)
    {
        listPanel.gameObject.SetActive(visible);
        if (panel)
        {
            panel.gameObject.SetActive(visible);
        }
    }

    private void ShowCancelButton(bool visible)
    {
        cancelButton.gameObject.SetActive(visible);
    }
}
