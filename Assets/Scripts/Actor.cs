using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public Animator animator;
    public UnityEvent<Actor, ActorEvent> onAction;

    private Coroutine _animateRoutine;

    public void Animate(string anim, float delay = 0f)
    {
        if (_animateRoutine != null)
        {
            StopCoroutine(_animateRoutine);
        }
        if (delay == 0f)
        {
            animator.Play(anim, 0, 0f);
        }
        else
        {
            animator.Play("Paused");
            _animateRoutine = StartCoroutine(AnimateRoutine(anim, delay));
        }
    }

    private IEnumerator AnimateRoutine(string anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(anim, 0, 0f);
        _animateRoutine = null;
    }

    public void Act(ActorEvent actorEvent)
    {
        onAction.Invoke(this, actorEvent);
    }

    private void Reset()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
}
