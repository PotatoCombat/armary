using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI weatherText;

    public Sprite Icon
    {
        get => icon.sprite;
        set => icon.sprite = value;
    }

    public void ShowWeather(string weather)
    {
        weatherText.text = weather;
    }
}
