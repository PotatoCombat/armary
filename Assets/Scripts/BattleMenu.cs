using UnityEngine;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public Battler user;
    public Team team;
    public BattleMenuPanel currentPanel;

    [Header("Components")]
    public GameObject buttons;
    public GameObject cancelButton;
    public BattleMenuPanel defaultPanel;
    public MoveTooltip moveTooltip;

    [Header("Events")]
    public UnityEvent<MoveData> onSelectMove;

    public void LoadContext(Battler user, Team team)
    {
        this.user = user;
        this.team = team;
    }

    public void ShowButtons(bool visible)
    {
        buttons.SetActive(visible);
    }

    public void ShowPanel(bool visible)
    {
        if (currentPanel)
        {
            currentPanel.Show(visible);
        }
    }

    public void ShowCancelButton(bool visible)
    {
        cancelButton.SetActive(visible);
    }

    public void ShowTooltip(MoveData move)
    {
        moveTooltip.LoadData(move);
        moveTooltip.SetVisible(true);
    }

    public void HideTooltip()
    {
        moveTooltip.SetVisible(false);
    }

    public void SelectPanel(BattleMenuPanel panel)
    {
        if (currentPanel)
        {
            currentPanel.Show(false);
        }
        currentPanel = panel;
        currentPanel.LoadContext(user, team);
        currentPanel.Show(true);
    }

    public void SelectDefaultPanel()
    {
        SelectPanel(defaultPanel);
    }

    public void SelectMove(MoveData move)
    {
        onSelectMove.Invoke(move);
    }
}
