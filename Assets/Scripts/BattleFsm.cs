using System;
using UnityEngine;

[Serializable]
public class BattleFsm
{
    public BattleState state;

    private BattleManager _controller;

    public void SetController(BattleManager controller)
    {
        _controller = controller;
    }

    public void ChangeState(BattleState state)
    {
        this.state?.Exit(_controller);
        this.state = state;
        this.state.Enter(_controller);
    }

    public void Update()
    {
        this.state?.Update(_controller);
    }
}

[Serializable]
public class BattleState
{
    public virtual void Enter(BattleManager controller)
    {
        Debug.Log(GetType().Name);
    }
    public virtual void Update(BattleManager controller) { }
    public virtual void Exit(BattleManager controller) { }
}

[Serializable]
public class StartBattleState : BattleState
{
    public override void Enter(BattleManager controller)
    {
        base.Enter(controller);
        controller.QueueAnimations(() =>
        {
            foreach (var battler in controller.allies)
            {
                if (battler.isActiveAndEnabled)
                {
                    battler.Spin();
                }
            }
        });
        foreach (var battler in controller.foes)
        {
            if (battler.isActiveAndEnabled)
            {
                controller.QueueAnimations(() => battler.Shake());
            }
        }
        controller.fsm.ChangeState(new AnimatingState());
    }
}

[Serializable]
public class AnimatingState : BattleState
{
    public override void Enter(BattleManager controller)
    {
        base.Enter(controller);
        controller.PlayNextAnimation();
    }
}

[Serializable]
public class AllyTurnState : BattleState
{
    public override void Enter(BattleManager controller)
    {
        base.Enter(controller);
        var info = "";
        foreach (var battler in controller.allies)
        {
            info += $"{battler}\n";
        }
        Debug.Log(info);
        controller.turn++;
    }
}

[Serializable]
public class FoeTurnState : BattleState
{
    public override void Enter(BattleManager controller)
    {
        base.Enter(controller);
        var info = "";
        foreach (var battler in controller.foes)
        {
            info += $"{battler}\n";
        }
        Debug.Log(info);
        controller.turn++;
    }
}

[Serializable]
public class WeatherTurnState : BattleState
{
    public override void Enter(BattleManager controller)
    {
        base.Enter(controller);
        controller.turn++;
    }
}
