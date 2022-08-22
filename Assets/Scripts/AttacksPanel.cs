using System.Collections.Generic;

public class AttacksPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;

    public override void LoadContext(Battler user, Team team)
    {
        base.LoadContext(user, team);
        var numMoves = user.data.moves.Length;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < numMoves)
            {
                buttons[i].SetData(user.data.moves[i]);
                buttons[i].Show(true);
            }
            else
            {
                buttons[i].Show(false);
            }
        }
    }
}
