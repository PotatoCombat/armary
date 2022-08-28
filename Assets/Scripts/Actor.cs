using System.Collections;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Animator animator;

    [Header("Events")]
    public SignalEvent fxEvent;
    public SignalEvent hitEvent;

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

    public void Execute(ActorCommand command)
    {
        command.Invoke(this);
    }

    public void Fx()
    {
        fxEvent.Raise(gameObject);
    }

    public void Hit()
    {
        hitEvent.Raise(gameObject);
    }

    private void Reset()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
}
