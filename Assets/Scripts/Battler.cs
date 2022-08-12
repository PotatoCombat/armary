using System.Collections;
using TMPro;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public Actor actor;

    public TextMeshProUGUI hpText;
    public BattlerData data;

    public bool IsAlive => true;
    public int Actions => 0;

    private Coroutine _delayedAnimationRoutine;

    private void Start()
    {
        Hide();
    }

    public void Load(BattlerData data)
    {
        this.data = data;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.data = null;
        gameObject.SetActive(false);
    }

    public void PlayAnimation(string anim)
    {
        actor.animator.Play(anim);
    }

    public void PlayDelayedAnimation(string anim, float delay)
    {
        if (_delayedAnimationRoutine != null)
        {
            StopCoroutine(_delayedAnimationRoutine);
        }
        _delayedAnimationRoutine = StartCoroutine(DelayedAnimationRoutine(anim, delay));
    }

    private IEnumerator DelayedAnimationRoutine(string anim, float delay)
    {
        actor.animator.Play("Paused");
        yield return new WaitForSeconds(delay);
        actor.animator.Play(anim);
    }

    // public void Spin()
    // {
    //     model.animator.Play("Spin Start");
    // }
    //
    // public void Shake()
    // {
    //     model.animator.Play("Shake");
    // }
    //
    // public void Attack1()
    // {
    //     model.animator.Play("Attack1_Charge");
    // }
    //
    // public void Hurt(int damage)
    // {
    //     model.animator.Play("Hurt");
    // }
}
