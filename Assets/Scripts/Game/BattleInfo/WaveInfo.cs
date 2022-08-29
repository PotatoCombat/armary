using TMPro;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    public void ShowWave(int wave, int maxWave)
    {
        waveText.text = $"{wave} / {maxWave}";
    }
}
