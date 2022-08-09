using UnityEngine;

public class MoveBehaviour : BattlerBehaviour
{
    public enum Position
    {
        Global, Local, Target, Allies, Foes
    }

    public Position position;
    public Vector2 offset;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (!Battler)
        {
            return;
        }
        var nextPosition = offset;
        switch (position)
        {
            case Position.Target:
                break;
            case Position.Allies:
                break;
            case Position.Foes:
                break;
            case Position.Local:
                nextPosition += (Vector2)Battler.transform.position;
                break;
            case Position.Global:
            default:
                break;
        }
        Battler.Move(nextPosition, stateInfo.length);
    }
}
