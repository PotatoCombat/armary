using TMPro;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public Actor actor;

    public TextMeshProUGUI hpText;
    public BattlerData data;

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
