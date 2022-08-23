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
                buttons[i].LoadData(user.data.moves[i]);
                buttons[i].LoadSprite(user.data.moves[i].sprite);
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
