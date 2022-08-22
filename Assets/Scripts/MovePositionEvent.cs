using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]
public class MovePositionEvent : ActorEvent
{
    private enum Type
    {
        Global, Local, Target, Allies, Foes
    }

    [SerializeField] private Type position;
    [SerializeField] private Vector2 offset;
    [SerializeField] private int duration;
    [SerializeField] private float frameRate = 60f;

    public override void Invoke(Actor actor, BattleManager manager)
    {
        var nextPosition = offset;
        switch (position)
        {
            case Type.Target:
                Transform target;
                if (manager.TargetBattler)
                {
                    target = manager.TargetBattler.transform;
                    nextPosition *= target.localScale.x;
                    nextPosition += (Vector2)target.position;
                }
                else if (manager.TargetTeam)
                {
                    target = manager.TargetTeam.battlers[0].transform;
                    nextPosition *= target.localScale.x;
                    nextPosition += (Vector2)target.position;
                }
                break;
            case Type.Allies:
                break;
            case Type.Foes:
                break;
            case Type.Local:
                nextPosition += (Vector2)actor.transform.parent.position;
                break;
            case Type.Global:
            default:
                break;
        }
        actor.transform.parent
            .DOMove(nextPosition, duration / frameRate)
            .SetEase(Ease.Linear);
    }
}
