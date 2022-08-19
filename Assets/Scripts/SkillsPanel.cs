using System.Collections.Generic;

public class SkillsPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;

    public override void Show(Battler user, Team team)
    {
        base.Show(user, team);
        var numMoves = user.data.moves.Length;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < numMoves)
            {
                buttons[i].Data = user.data.moves[i];
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
