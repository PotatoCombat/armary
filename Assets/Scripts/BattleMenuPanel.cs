using UnityEngine;

public abstract class BattleMenuPanel : MonoBehaviour
{
    [SerializeField] protected SimpleButton menuButton;

    public virtual void LoadContext(Battler user, Team team)
    {

    }

    public virtual void Show(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
