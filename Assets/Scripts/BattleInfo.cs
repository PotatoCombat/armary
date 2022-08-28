using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    [SerializeField] private BattleContext context;

    public WeatherInfo weatherInfo;
    public WaveInfo waveInfo;
    public List<HealthBar> playerInfos;
    public List<HealthBar> npcInfos;

    public void UpdateWaveInfo(int wave, int maxWave)
    {
        waveInfo.ShowWave(wave, maxWave);
    }

    public void UpdateWeatherInfo()
    {
        weatherInfo.ShowWeather("Normal");
    }

    public void UpdatePlayerInfos()
    {
        UpdateHealthBars(context.PlayerTeam, playerInfos);
    }

    public void UpdateNpcInfos()
    {
        UpdateHealthBars(context.NpcTeam, npcInfos);
    }

    private void UpdateHealthBars(Team team, List<HealthBar> healthBars)
    {
        for (var i = 0; i < team.battlers.Count; i++)
        {
            var battler = team.battlers[i];
            var healthBar = healthBars[i];
            if (battler.isActiveAndEnabled)
            {
                healthBar.ShowHp(battler.data.hp, battler.data.maxHp);
                healthBar.gameObject.SetActive(true);
            }
            else
            {
                healthBar.gameObject.SetActive(false);
            }
        }
    }
}
