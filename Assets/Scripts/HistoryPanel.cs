using System.Collections.Generic;

public class HistoryPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;

    public override void Show(Battler user, Team team)
    {
        base.Show(user, team);
    }
}
