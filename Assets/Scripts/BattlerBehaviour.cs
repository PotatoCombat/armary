using UnityEngine;

public abstract class BattlerBehaviour : StateMachineBehaviour
{
    protected Battler Battler;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadBattler(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LoadBattler(animator);
    }

    private void LoadBattler(Animator animator)
    {
        if (!Battler)
        {
            Battler = animator.GetComponentInParent<Battler>();
            if (!Battler)
            {
                Debug.LogWarning($"{animator.gameObject} must have a parent with a Battler component");
            }
        }
    }
}
