using UnityEngine;

public class ListPanel : BattleMenuPanel
{
    [Header("Buttons")]
    [SerializeField] private SimpleButton attack;
    [SerializeField] private SimpleButton tactic;
    [SerializeField] private SimpleButton skill;
    [SerializeField] private SimpleButton equip;
    [SerializeField] private SimpleButton item;

    [Header("Alternatives")]
    [SerializeField] private MoveButton attackAlt;
    public MoveType attackMove;

    public override void LoadContext(Battler user, Team team)
    {
        var useAlternativeAttack = user.data.moves.Count <= 1;
        attack.gameObject.SetActive(!useAlternativeAttack);
        attackAlt.gameObject.SetActive(useAlternativeAttack);
        attackAlt.LoadData(attackMove);
    }
}
