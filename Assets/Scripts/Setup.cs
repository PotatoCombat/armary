using UnityEngine;

[DisallowMultipleComponent]
public class Setup : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Destroy(this);
    }
}
