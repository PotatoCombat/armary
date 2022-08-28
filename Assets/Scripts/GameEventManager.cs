using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    // public List<BattlerEventListener> battlerEvents;
    // public List<TeamEventListener> teamEvents;

    private void OnEnable()
    {
        // foreach (var listener in battlerEvents)
        // {
        //     listener.OnEnable();
        // }
        // foreach (var listener in teamEvents)
        // {
        //     listener.OnEnable();
        // }
    }

    private void OnDisable()
    {
        // foreach (var listener in battlerEvents)
        // {
        //     listener.OnDisable();
        // }
        // foreach (var listener in teamEvents)
        // {
        //     listener.OnDisable();
        // }
    }

    [ContextMenu("Validate")]
    private void OnManualValidate()
    {
        // foreach (var listener in battlerEvents)
        // {
        //     listener.OnValidate();
        // }
        // foreach (var listener in teamEvents)
        // {
        //     listener.OnValidate();
        // }
    }
}
