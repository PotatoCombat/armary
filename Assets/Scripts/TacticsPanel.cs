using System.Collections.Generic;

public class TacticsPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;
    public MoveData defend;
    public MoveData scan;
    public MoveData skip;
    public MoveData flee;
    public MoveData swap1;
    public MoveData swap2;

    public override void Show(Battler user, Team team)
    {
        base.Show(user, team);
        buttons[0].Data = defend;
        buttons[1].Data = scan;
        buttons[2].Data = skip;
        buttons[3].Data = flee;
        buttons[4].Data = swap1;
        buttons[5].Data = swap2;
    }
}
