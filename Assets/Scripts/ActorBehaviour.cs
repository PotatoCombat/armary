using UnityEngine;

public abstract class ActorBehaviour : StateMachineBehaviour
{
    protected Actor Actor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadActor(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadActor(animator);
    }

    private void LoadActor(Animator animator)
    {
        if (!Actor && !animator.TryGetComponent(out Actor))
        {
            Debug.LogWarning($"{animator.gameObject} must have a parent with an {nameof(Actor)} component");
        }
    }
}
