using UnityEngine;
using UnityEngine.Events;

public abstract class BattleMenuPanel : MonoBehaviour
{

    public virtual void Show(Battler user, Team team)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
