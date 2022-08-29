using System.Collections.Generic;

public class SkillsPanel : BattlePanel
{
    public List<MoveButton> buttons;

    public override void Load()
    {
        var numMoves = context.User.data.moves.Count;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < numMoves)
            {
                // buttons[i].Data = user.data.moves[i];
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
