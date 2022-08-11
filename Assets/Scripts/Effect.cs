using UnityEngine;
using UnityEngine.Events;

public class Effect : MonoBehaviour
{
    public Actor actor;

    public UnityEvent<Actor, ActorEvent> OnAnimationEvent => actor.onAnimationEvent;
}
