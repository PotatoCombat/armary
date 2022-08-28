using UnityEngine;

public class SignalEventBehaviour : StateMachineBehaviour
{
    [SerializeField] private SignalEvent onEnter;
    [SerializeField] private SignalEvent onExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onEnter.Raise(animator.gameObject);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onExit.Raise(animator.gameObject);
    }
}
