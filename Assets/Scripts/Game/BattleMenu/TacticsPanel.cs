using System.Collections.Generic;

public class TacticsPanel : BattlePanel
{
    public List<MoveButton> buttons;
    public MoveType defend;
    public MoveType scan;
    public MoveType skip;
    public MoveType flee;
    public MoveType swap1;
    public MoveType swap2;

    public override void Load()
    {
        buttons[0].LoadData(defend);
        buttons[1].LoadData(scan);
        buttons[2].LoadData(skip);
        buttons[3].LoadData(flee); // if Battler.canFlee
        buttons[4].LoadData(swap1);
        buttons[5].LoadData(swap2);
    }
}
