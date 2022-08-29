using UnityEngine;

public class BattleEvents : MonoBehaviour
{
    public BattleManager manager;

    [Header("Events")]
    public GameObjectEvent onIdle;
    public GameObjectEvent onBusy;
    public GameEvent onFx;
    public GameEvent onHit;

    public BattlerEvent onSelectUser;
    public MoveTypeEvent onSelectMove;
    public GameEvent onCancelMove;
    public BattlerEvent onTargetBattler;
    public TeamEvent onTargetTeam;

    private void OnEnable()
    {
        onIdle.AddListener(manager.NotifyIdle);
        onBusy.AddListener(manager.NotifyBusy);
        onFx.AddListener(manager.PerformFx);
        onHit.AddListener(manager.PerformHit);

        onSelectUser.AddListener(manager.SelectUser);
        onSelectMove.AddListener(manager.SelectMove);
        onCancelMove.AddListener(manager.CancelMove);
        onTargetBattler.AddListener(manager.SelectTarget);
        onTargetTeam.AddListener(manager.SelectTarget);
    }

    private void OnDisable()
    {
        onIdle.RemoveListener(manager.NotifyIdle);
        onBusy.RemoveListener(manager.NotifyBusy);
        onFx.RemoveListener(manager.PerformFx);
        onHit.RemoveListener(manager.PerformHit);

        onSelectUser.RemoveListener(manager.SelectUser);
        onSelectMove.RemoveListener(manager.SelectMove);
        onCancelMove.RemoveListener(manager.CancelMove);
        onTargetBattler.RemoveListener(manager.SelectTarget);
        onTargetTeam.RemoveListener(manager.SelectTarget);
    }
}
