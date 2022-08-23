using UnityEngine;
using UnityEngine.Events;

public class HitEventTrigger : MonoBehaviour
{
    public UnityEvent<HitEvent> onHit;

    public void Hit(HitEvent hitEvent)
    {
        onHit.Invoke(hitEvent);
    }
}
