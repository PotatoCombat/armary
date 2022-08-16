using TMPro;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI maxWave;

    private int _wave = 1;
    private int _maxWave = 1;

    public int Wave
    {
        get => _wave;
        set
        {
            _wave = value;
            UpdateWaves();
        }
    }

    public int MaxWave
    {
        get => _maxWave;
        set
        {
            _maxWave = value;
            UpdateWaves();
        }
    }

    private void UpdateWaves()
    {
        wave.text = _wave.ToString();
        maxWave.text = _maxWave.ToString();
    }
}
