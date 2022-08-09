using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    public BattlerModel model;

    public TextMeshProUGUI hpText;
    public BattlerData data;

    public UnityEvent<Battler> onClick;
    public UnityEvent<Battler, BattlerAnimationEvent> onAnimationEvent;

    private void Start()
    {
        model.onAction.RemoveListener(RaiseAction);
        model.onAction.AddListener(RaiseAction);
    }

    public void SetData(BattlerData data)
    {
        this.data = data;
    }

    public void ClearData()
    {
        this.data = null;
    }

    public void PlayAnimation(string anim)
    {
        model.animator.Play(anim);
    }

    public void Spin()
    {
        model.animator.Play("Spin Start");
    }

    public void Shake()
    {
        model.animator.Play("Shake");
    }

    public void Attack1()
    {
        model.animator.Play("Attack1_Charge");
    }

    public void Hurt(int damage)
    {
        model.animator.Play("Hurt");
    }

    public void Move(Vector2 position, float duration)
    {
        model.transform
            .DOMove(position, duration)
            .SetEase(Ease.Linear);
    }

    public void Click()
    {
        onClick?.Invoke(this);
    }

    public void RaiseAnimating(bool animating)
    {
        onAnimationEvent?.Invoke(this, animating ? BattlerAnimationEvent.Busy : BattlerAnimationEvent.Idle);
    }

    private void RaiseAction()
    {
        onAnimationEvent?.Invoke(this, BattlerAnimationEvent.Act);
    }
}
