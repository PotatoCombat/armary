using UnityEngine;
using UnityEngine.Events;

public class BattlerModel : MonoBehaviour
{
    public Animator animator;
    public UnityEvent onAction;

    public float GetAnimationDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void Act()
    {
        onAction?.Invoke();
    }

    private void Reset()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
}
