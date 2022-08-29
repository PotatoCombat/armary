using System.Collections.Generic;

public class AttacksPanel : BattlePanel
{
    public List<MoveButton> buttons;

    public override void Load()
    {
        var numMoves = context.User.data.moves.Count;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < numMoves)
            {
                buttons[i].LoadData(context.User.data.moves[i]);
                buttons[i].LoadSprite(context.User.data.moves[i].sprite);
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
