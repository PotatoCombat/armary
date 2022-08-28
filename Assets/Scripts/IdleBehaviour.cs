using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private GameObjectEvent onEnter;
    [SerializeField] private GameObjectEvent onExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onEnter.Raise(animator.gameObject);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onExit.Raise(animator.gameObject);
    }
}
