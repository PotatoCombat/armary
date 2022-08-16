using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI weather;

    public WeatherData data;

    private void OnValidate()
    {
        if (data)
        {
            icon.sprite = data.icon;
            weather.text = data.name;
        }
        else
        {
            icon.sprite = null;
            weather.text = " --- ";
        }
    }
}
