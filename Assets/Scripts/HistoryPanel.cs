using System.Collections.Generic;

public class HistoryPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;

    public override void LoadContext(Battler user, Team team)
    {
        base.LoadContext(user, team);
    }
}
