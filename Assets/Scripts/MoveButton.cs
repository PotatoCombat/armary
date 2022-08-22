using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

public class MoveButton : MonoBehaviour
{
    public MoveData data;
    public SimpleButton button;

    public UnityEvent<MoveData> onClick;
    public UnityEvent<MoveData> onHoverEnter;
    public UnityEvent<MoveData> onHoverExit;

    public void SetData(MoveData data)
    {
        this.data = data;
        this.button.image.sprite = data.sprite;
    }

    public void Show(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void Click()
    {
        onClick.Invoke(data);
    }

    public void HoverEnter()
    {
        onHoverEnter.Invoke(data);
    }

    public void HoverExit()
    {
        onHoverExit.Invoke(data);
    }

#if UNITY_EDITOR
    private void Reset()
    {
        button = GetComponent<SimpleButton>();
        button.onClick = new UnityEvent();
        button.onHoverEnter = new UnityEvent();
        button.onHoverExit = new UnityEvent();

        UnityEventTools.AddPersistentListener(button.onClick, Click);
        UnityEventTools.AddPersistentListener(button.onHoverEnter, HoverEnter);
        UnityEventTools.AddPersistentListener(button.onHoverExit, HoverExit);

        EditorUtility.SetDirty(button);
    }
#endif
}
