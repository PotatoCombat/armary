using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[SelectionBase]
[DisallowMultipleComponent]
public class SimpleButton : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] private bool interactable = true;

    public Image image;
    public Color normalColor = new Color32(255, 255, 255, 255);
    public Color hoverColor = new Color32(200, 200, 200, 255);
    public Color disabledColor = new Color32(100, 100, 100, 255);
    public float fadeDuration;

    [Space]
    public UnityEvent onClick;
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    public bool Interactable
    {
        get => interactable;
        set
        {
            interactable = value;
            StartColorTween(interactable ? normalColor : disabledColor, 0f);
        }
    }

    private void OnEnable()
    {
        StartColorTween(normalColor, fadeDuration);
    }

    private void OnDisable()
    {
        StartColorTween(Color.white, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActiveAndEnabled && Interactable)
        {
            onClick.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActiveAndEnabled && Interactable)
        {
            onHoverEnter.Invoke();
            StartColorTween(hoverColor, fadeDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActiveAndEnabled && Interactable)
        {
            onHoverExit.Invoke();
            StartColorTween(normalColor, fadeDuration);
        }
    }

    private void StartColorTween(Color targetColor, float duration)
    {
        if (!image)
        {
            return;
        }
        image.CrossFadeColor(targetColor, duration, true, true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        StartColorTween(interactable ? normalColor : disabledColor, 0f);
    }

    private void Reset()
    {
        image = GetComponent<Image>();
        StartColorTween(normalColor, fadeDuration);
    }
#endif
}
