using UnityEngine;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public Battler user;
    public Team team;
    public BattleMenuPanel currentPanel;

    [Header("Components")]
    public BattleMenuPanel defaultPanel;
    public MoveTooltip moveTooltip;

    [Header("Events")]
    public UnityEvent<MoveData> onSelectMove;

    public void LoadContext(Battler user, Team team)
    {
        this.user = user;
        this.team = team;
        this.currentPanel = defaultPanel;
    }

    public void Show(bool visible)
    {
        gameObject.SetActive(visible);
        if (currentPanel)
        {
            if (visible)
            {
                currentPanel.LoadContext(user, team);
            }
            currentPanel.Show(visible);
        }
    }

    public void ShowPanel(BattleMenuPanel panel)
    {
        if (currentPanel)
        {
            currentPanel.Show(false);
        }
        currentPanel = panel;
        currentPanel.LoadContext(user, team);
        currentPanel.Show(true);
    }

    public void ShowMoveTooltip(MoveData move)
    {
        moveTooltip.Show();
    }

    public void HideMoveTooltip(MoveData move)
    {
        moveTooltip.Hide();
    }

    public void SelectMove(MoveData move)
    {
        onSelectMove.Invoke(move);
    }
}
