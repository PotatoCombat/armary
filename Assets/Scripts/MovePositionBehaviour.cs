using UnityEngine;

public class MovePositionBehaviour : ActorBehaviour
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
        if (!Actor)
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
                nextPosition += (Vector2)Actor.transform.position;
                break;
            case Position.Global:
            default:
                break;
        }
        Actor.Move(nextPosition, stateInfo.length);
    }
}
