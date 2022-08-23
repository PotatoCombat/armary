using System.Collections.Generic;

public class AttacksPanel : BattleMenuPanel
{
    public List<MoveButton> buttons;

    public override void LoadContext(Battler user, Team team)
    {
        base.LoadContext(user, team);
        var numMoves = user.data.moves.Count;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < numMoves)
            {
                buttons[i].LoadData(user.data.moves[i]);
                buttons[i].LoadSprite(user.data.moves[i].sprite);
                buttons[i].gameObject.SetActive(true);
                /*
                 * var cooldown = user.GetMoveCooldown(move);
                 * if (cooldown > 0) buttons[i].ShowCooldown(cooldown);
                 */
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
