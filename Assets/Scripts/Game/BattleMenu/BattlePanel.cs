using UnityEngine;

public abstract class BattlePanel : MonoBehaviour
{
    [SerializeField] protected BattleContext context;

    public abstract void Load();
}
