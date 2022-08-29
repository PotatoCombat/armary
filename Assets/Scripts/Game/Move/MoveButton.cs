using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

public class MoveButton : MonoBehaviour
{
    public MoveType data;

    [Header("Components")]
    public SimpleButton button;
    public MoveTypeEvent onClick;
    public MoveTypeEvent onHoverEnter;
    public BattleEvent onHoverExit;

    public void LoadData(MoveType data)
    {
        this.data = data;
    }

    public void LoadSprite(Sprite sprite)
    {
        button.image.sprite = sprite;
    }

    public void Click()
    {
        onClick.Raise(data);
    }

    public void HoverEnter()
    {
        onHoverEnter.Raise(data);
    }

    public void HoverExit()
    {
        onHoverExit.Raise();
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
