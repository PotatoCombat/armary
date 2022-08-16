using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherInfo : MonoBehaviour
{
    [SerializeField] private Image weatherIcon;
    [SerializeField] private TextMeshProUGUI weather;
    [SerializeField] private TextMeshProUGUI waves;

    public void SetWeather(string weather)
    {
        this.weather.text = $"{weather}";
        this.weatherIcon = null;
    }

    public void SetWaves(int wave, int maxWaves)
    {
        this.waves.text = $"{wave}/{maxWaves}";
    }
}
