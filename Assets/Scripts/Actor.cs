using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public Animator animator;
    public UnityEvent<Actor, ActorEvent> onAnimationEvent;

    public void Idle()
    {
        onAnimationEvent?.Invoke(this, ActorEvent.Idle);
    }

    public void Busy()
    {
        onAnimationEvent?.Invoke(this, ActorEvent.Busy);
    }

    public void Hit()
    {
        onAnimationEvent?.Invoke(this, ActorEvent.Hit);
    }

    public void Effect()
    {
        onAnimationEvent?.Invoke(this, ActorEvent.Effect);
    }

    public void Move(Vector2 position, float duration)
    {
        transform
            .DOMove(position, duration)
            .SetEase(Ease.Linear);
    }

    private void Reset()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
}
