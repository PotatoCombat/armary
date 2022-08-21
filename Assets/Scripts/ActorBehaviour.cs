using UnityEngine;

public class ActorBehaviour : StateMachineBehaviour
{
    [SerializeField] private ActorEvent onEnter;
    [SerializeField] private ActorEvent onExit;

    private Actor _actor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadActor(animator);
        _actor.Act(onEnter);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadActor(animator);
        _actor.Act(onExit);
    }

    private void LoadActor(Animator animator)
    {
        if (!_actor && !animator.TryGetComponent(out _actor))
        {
            Debug.LogWarning($"{animator.gameObject} must have a parent with an {nameof(Actor)} component");
        }
    }
}
