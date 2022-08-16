using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTargets : MonoBehaviour
{
    public HoverButton all;
    public HoverButton allPlayers;
    public HoverButton allNpcs;
    public List<HoverButton> players;
    public List<HoverButton> npcs;

    public UnityEvent<int> onClick;
    public UnityEvent<int, Vector2> onHoverEnter;
    public UnityEvent<int, Vector2> onHoverExit;

    // public void Show(Action<HoverButton, int> reducer = null)
    // {
    //     gameObject.SetActive(true);
    //     if (reducer != null)
    //     {
    //         for (var i = 0; i < buttons.Count; i++)
    //         {
    //             reducer.Invoke(buttons[i], i);
    //         }
    //     }
    // }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Click(HoverButton button)
    {
        onClick.Invoke(button.index);
    }

    private void HoverEnter(HoverButton button)
    {
        onHoverEnter.Invoke(button.index, button.transform.position);
    }

    private void HoverExit(HoverButton button)
    {
        onHoverExit.Invoke(button.index, button.transform.position);
    }
}
