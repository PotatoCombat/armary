using System;
using Random = UnityEngine.Random;

[Serializable]
public class MoveLogic
{
    public void Play(BattleManager controller)
    {
        var user = controller.user;
        var target = controller.targets[0];
        controller.QueueAnimations(
            () => user.Attack1(),
            () => target.Hurt(Random.Range(-100, 100))
        );
    }
}
