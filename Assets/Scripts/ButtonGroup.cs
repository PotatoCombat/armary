using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using System.Linq;
using PotatoEditor;
using UnityEditor;
using UnityEditor.Events;
#endif

public class ButtonGroup : MonoBehaviour
{
    public List<HoverButton> buttons;

    [Space]
    public UnityEvent<int> onClick;
    public UnityEvent<int, Vector2> onHoverEnter;
    public UnityEvent<int, Vector2> onHoverExit;

    public void Show(Action<HoverButton, int> setButton = null)
    {
        gameObject.SetActive(true);
        if (setButton != null)
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                setButton.Invoke(buttons[i], i);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Click(HoverButton button)
    {
        // onClick.Invoke(button.index);
    }

    private void HoverEnter(HoverButton button)
    {
        // onHoverEnter.Invoke(button.index, button.transform.position);
    }

    private void HoverExit(HoverButton button)
    {
        // onHoverExit.Invoke(button.index, button.transform.position);
    }

#if UNITY_EDITOR
    [ContextMenu("Fetch Buttons")]
    private void FetchButtons()
    {
        buttons = GetComponentsInChildren<HoverButton>(true).ToList();
        for (var i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            // button.index = i;

            UnityEventExtensions.RemoveAllPersistentListeners(button.onClick);
            UnityEventExtensions.RemoveAllPersistentListeners(button.onHoverEnter);
            UnityEventExtensions.RemoveAllPersistentListeners(button.onHoverExit);

            UnityEventTools.AddObjectPersistentListener(button.onClick, Click, button);
            UnityEventTools.AddObjectPersistentListener(button.onHoverEnter, HoverEnter, button);
            UnityEventTools.AddObjectPersistentListener(button.onHoverExit, HoverExit, button);

            EditorUtility.SetDirty(button);
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
