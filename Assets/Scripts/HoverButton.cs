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
    public Graphic targetGraphic;
    public Color normalColor;
    public Color hoverColor;
    public float fadeDuration;

    [Header("Audio")]
    public AudioSource targetAudio;
    public AudioClip hoverSfx;
    public AudioClip clickSfx;

    [Space]
    public UnityEvent onClick;

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
        PlaySfx(hoverSfx);
        StartColorTween(hoverColor, fadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        _withinGraphics = false;
        StartColorTween(normalColor, fadeDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        PlaySfx(clickSfx);
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
        onClick.Invoke();
    }

    private bool IsInteractable()
    {
        return isActiveAndEnabled && interactable;
    }

    private void StartColorTween(Color targetColor, float duration)
    {
        if (!targetGraphic)
        {
            return;
        }
        targetGraphic.CrossFadeColor(targetColor, duration, true, true);
    }

    private void PlaySfx(AudioClip sfx)
    {
        if (!targetAudio)
        {
            return;
        }
        targetAudio.PlayOneShot(sfx);
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
        targetGraphic = GetComponent<Graphic>();
        targetAudio = GetComponent<AudioSource>();
    }
#endif
}