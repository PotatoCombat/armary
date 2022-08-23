using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI weather;

    public WeatherData data;
}
