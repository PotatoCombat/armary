using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]
public class ActorMoveCommand : ActorCommand
{
    [SerializeField] private Vector3Value position;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float duration;
    [SerializeField] private float frameRate = 60f;
    [SerializeField] private Ease ease = Ease.Linear;

    public override void Invoke(Actor actor)
    {
        actor.transform.parent
            .DOMove(position.Value + offset * actor.transform.localScale.x, duration / frameRate)
            .SetEase(ease);
    }

    // Move Back command
}
