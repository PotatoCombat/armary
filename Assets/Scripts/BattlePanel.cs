using UnityEngine;

public abstract class BattlePanel : MonoBehaviour
{
    [SerializeField] protected BattleContext context;

    protected abstract void OnEnable();
}
