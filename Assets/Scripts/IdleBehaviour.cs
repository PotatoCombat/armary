using UnityEngine;

public class IdleBehaviour : BattlerBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (Battler)
        {
            Battler.RaiseAnimating(false);
            //Battler.Act();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (Battler)
        {
            Battler.RaiseAnimating(true);
        }
    }
}
