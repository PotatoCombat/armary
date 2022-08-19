using UnityEngine;

[DisallowMultipleComponent]
public class DisableMultiTouch : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Destroy(this);
    }
}
