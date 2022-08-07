using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    public Animator animator;
    public BattlerData data;
    public UnityEvent<Battler> onClick;

    public void PlayAnimation(string anim)
    {
        animator.Play(anim);
    }

    public void SetData(BattlerData data)
    {
        this.data = data;
    }

    public void ClearData()
    {
        this.data = null;
    }

    public void Click()
    {
        onClick.Invoke(this);
    }
}
