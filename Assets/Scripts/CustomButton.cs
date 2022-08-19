using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[SelectionBase]
[DisallowMultipleComponent]
public abstract class CustomButton<T> : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    [SerializeField] private bool interactable = true;
    [SerializeField] private T data;

    public Image targetImage;
    public Color normalColor = new Color32(255, 255, 255, 255);
    public Color hoverColor = new Color32(200, 200, 200, 255);
    public Color disabledColor = new Color32(100, 100, 100, 255);
    public float fadeDuration;

    [Space]
    public UnityEvent<T> onClick;
    public UnityEvent<T, PointerEventData> onHoverEnter;
    public UnityEvent<T, PointerEventData> onHoverExit;

    private bool _withinGraphics;

    public bool Interactable
    {
        get => interactable;
        set
        {
            interactable = value;
            StartColorTween(interactable ? normalColor : disabledColor, 0f);
        }
    }

    public T Data
    {
        get => data;
        set => SetData(value);
    }

    protected virtual void SetData(T data)
    {
        this.data = data;
    }

    protected virtual void OnEnable()
    {
        _withinGraphics = false;
        StartColorTween(normalColor, fadeDuration);
    }

    protected virtual void OnDisable()
    {
        _withinGraphics = false;
        StartColorTween(Color.white, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _withinGraphics = true;
        if (isActiveAndEnabled && Interactable)
        {
            onHoverEnter.Invoke(data, eventData);
            StartColorTween(hoverColor, fadeDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _withinGraphics = false;
        if (isActiveAndEnabled && Interactable)
        {
            onHoverExit.Invoke(data, eventData);
            StartColorTween(normalColor, fadeDuration);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_withinGraphics && isActiveAndEnabled && Interactable)
        {
            onClick.Invoke(data);
        }
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
    protected virtual void OnValidate()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        StartColorTween(interactable ? normalColor : disabledColor, 0f);
    }

    protected virtual void Reset()
    {
        targetImage = GetComponent<Image>();
        StartColorTween(normalColor, fadeDuration);
    }
#endif
}
