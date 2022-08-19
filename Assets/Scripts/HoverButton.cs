using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[SelectionBase]
[DisallowMultipleComponent]
public class HoverButton : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    public bool interactable;

    [Header("Graphics")]
    public Image targetImage;
    public Color normalColor;
    public Color hoverColor;
    public float fadeDuration;

    [Space]
    public UnityEvent<PointerEventData> onClick;
    public UnityEvent<PointerEventData> onHoverEnter;
    public UnityEvent<PointerEventData> onHoverExit;

    private bool _withinGraphics;

    private void OnEnable()
    {
        _withinGraphics = false;
        StartColorTween(normalColor, fadeDuration);
    }

    private void OnDisable()
    {
        _withinGraphics = false;
        StartColorTween(Color.white, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        _withinGraphics = true;
        onHoverEnter.Invoke(eventData);
        StartColorTween(hoverColor, fadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        _withinGraphics = false;
        onHoverExit.Invoke(eventData);
        StartColorTween(normalColor, fadeDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        if (!_withinGraphics)
        {
            return;
        }
        onClick.Invoke(eventData);
    }

    private bool IsInteractable()
    {
        return isActiveAndEnabled && interactable;
    }

    private void StartColorTween(Color targetColor, float duration)
    {
        if (!targetImage)
        {
            return;
        }
        targetImage.CrossFadeColor(targetColor, duration, true, true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        StartColorTween(normalColor, fadeDuration);
    }

    private void Reset()
    {
        targetImage = GetComponent<Image>();
    }
#endif
}
