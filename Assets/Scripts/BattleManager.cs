using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleData data;

    // Runtime
    public int Wave { get; private set; }
    private List<GameObject> _animating;
    private List<HitCommand> _hits;

    // Components
    [SerializeField] private BattleContext context;
    [SerializeField] private BattleFsm fsm;
    [SerializeField] private BattleEvents events;
    [SerializeField] private BattleStage stage;
    [SerializeField] private BattleInfo info;
    [SerializeField] private BattleMenu menu;

    public BattleContext Context => context;

    private void Start()
    {
        StartBattle();
    }

    public void LoadParty()
    {
        context.PlayerTeam.Load(data.party);
        info.UpdatePlayerInfos();
    }

    public void LoadWave(int wave)
    {
        var maxWaves = data.encounter.waves.Count;
        if (wave < 0 || wave >= maxWaves)
        {
            return;
        }
        Wave = wave;
        context.NpcTeam.Load(data.encounter.waves[wave]);
        info.UpdateNpcInfos();
        info.UpdateWaveInfo(wave + 1, maxWaves);
    }

    public void LoadPreviousWave()
    {
        LoadWave(--Wave);
    }

    public void LoadNextWave()
    {
        LoadWave(++Wave);
    }

    public void ShowPlayers()
    {
        // stage.LoadData(data);
    }

    public void ShowNpcs()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    // public void SwapBattler(int index, BattlerData battlerData)
    // {
    //     if (currentAllies.Length < index)
    //     {
    //         currentAllies[index].Load(battlerData);
    //     }
    // }

    public void SpawnBattler(BattlerTemplate battlerTemplate)
    {
        foreach (var ally in stage.allyTeam.battlers)
        {
            if (ally.data == null)
            {
                ally.LoadData(battlerTemplate.CreateBattlerData());
                return;
            }
        }
    }

    // The following methods trigger UI side effects.

    public void StartBattle()
    {
        Wave = -1;
        _animating = new List<GameObject>();
        _hits = new List<HitCommand>();
        fsm.Load(this);
        fsm.Init();
    }

    public void ResetContext()
    {
        context.AllyTeam = null;
        context.FoeTeam = null;
        context.User = null;
        context.Move = null;
        context.TargetBattler = null;
        context.TargetTeam = null;
    }

    private void ApplyDecision(BattleDecision decision)
    {
        context.User = decision.User;
        context.Move = decision.Move;
        context.TargetBattler = decision.TargetBattler;
        context.TargetTeam = decision.TargetTeam;
    }

    public void SelectUser(Battler user)
    {
        Debug.Log($"Selected User: {user}");
        context.User = user;
        if (user.IsAlive)
        {
            ShowUi(user);
        }
        else
        {
            PerformAiTurn(user);
        }
    }

    public void ShowUi(Battler user)
    {
        Debug.Log($"Show Ui: {user}");
        stage.LoadContext(context.AllyTeam, context.FoeTeam);
        stage.ShowPickers();
        context.User.ShowPicker(false);
        menu.LoadContext(context.User, context.AllyTeam);
        menu.SelectDefaultPanel();
        menu.Show();
    }

    public void SelectMove(MoveType move)
    {
        Debug.Log($"Selected Move: {move.name}");
        context.Move = move;
        stage.HidePickers();
        stage.ShowTargets(context.User, context.Move.target);
    }

    public void CancelMove()
    {
        Debug.Log($"Cancelled move");
        context.Move = null;
        stage.HideTargets();
        stage.ShowPickers();
        context.User.ShowPicker(false);
    }

    public void SelectTarget(Battler battler)
    {
        Debug.Log($"Target Battler: {battler}");
        context.TargetBattler = battler;
        PerformMove();
    }

    public void SelectTarget(Team team)
    {
        Debug.Log($"Target Team: {team}");
        context.TargetTeam = team;
        PerformMove();
    }

    public void PerformAiTurn(Battler user)
    {
        Debug.Log($"Run Ai: {user}");
        var decision = context.User.data.type.ai.GetTurnDecision(context); // TODO: reduce dot chain
        if (decision == null)
        {
            fsm.Next();
            return;
        }
        ApplyDecision(decision);
        PerformMove();
    }

    public void PerformAiCounter(Battler user)
    {
        Debug.Log($"Run Ai: {user}");
        var decision = context.User.data.type.ai.GetCounterDecision(context); // TODO: reduce dot chain
        if (decision == null)
        {
            fsm.Next();
            return;
        }
        ApplyDecision(decision);
        PerformMove();
    }

    private void PerformMove()
    {
        _hits = context.Move.logic.CreateHits(context);
        context.User.model.Animate(context.Move.animation);
        stage.HideTargets();
        menu.Hide();
    }

    public void PerformFx()
    {
        context.User.effects.Animate(context.Move.fx);
    }

    public void PerformHit()
    {
        _hits[0].Execute();
        _hits.RemoveAt(0);
        // info.UpdatePlayerInfos();
        // info.UpdateNpcInfos();
    }

    public void NotifyIdle(GameObject obj)
    {
        var completedAnimations = _animating.Remove(obj) && _animating.Count <= 0;
        if (completedAnimations)
        {
            fsm.Next();
        }
    }

    public void NotifyBusy(GameObject obj)
    {
        _animating.Add(obj);
    }
}
