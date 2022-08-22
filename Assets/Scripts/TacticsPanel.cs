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

    public override void LoadContext(Battler user, Team team)
    {
        base.LoadContext(user, team);
        buttons[0].SetData(defend);
        buttons[1].SetData(scan);
        buttons[2].SetData(skip);
        buttons[3].SetData(flee); // if Battler.canFlee
        buttons[4].SetData(swap1);
        buttons[5].SetData(swap2);
    }
}
