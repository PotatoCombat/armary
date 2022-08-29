using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleData data;

    [Header("Runtime")]
    [SerializeField] private int wave;
    [SerializeField] private List<GameObject> animating;
    [SerializeField] private List<HitCommand> hits;

    [Header("Components")]
    [SerializeField] private BattleContext context;
    [SerializeField] private BattleFsm fsm;
    [SerializeField] private BattleEvents events;
    [SerializeField] private BattlePosition positions;
    [SerializeField] private BattleStage stage;
    [SerializeField] private BattleInfo info;
    [SerializeField] private BattleMenu menu;
    [SerializeField] private BattleTooltips tooltips;

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
        this.wave = wave;
        context.NpcTeam.Load(data.encounter.waves[wave]);
        info.UpdateNpcInfos();
        info.UpdateWaveInfo(wave + 1, maxWaves);
    }

    public void LoadPreviousWave()
    {
        LoadWave(--wave);
    }

    public void LoadNextWave()
    {
        LoadWave(++wave);
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
        foreach (var ally in context.AllyTeam.battlers)
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
        wave = -1;
        animating = new List<GameObject>();
        hits = new List<HitCommand>();
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
        context.Move = decision.Move;
        context.TargetBattler = decision.TargetBattler;
        context.TargetTeam = decision.TargetTeam;
    }

    public void SelectUser(Battler user)
    {
        Debug.Log($"Selected User: {user}");
        context.User = user;
        if (user.IsControllable)
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
        stage.ShowPickers();
        menu.Load();
        menu.ShowDefaultPanel();
        menu.ShowInterface(true);
        menu.ShowCancelButton(false);
    }

    public void SelectMove(MoveType move)
    {
        Debug.Log($"Selected Move: {move.name}");
        context.Move = move;
        stage.HidePickers();
        stage.ShowTargets();
        menu.ShowInterface(false);
        menu.ShowCancelButton(true);
        tooltips.HideAll();
    }

    public void CancelMove()
    {
        Debug.Log($"Cancelled move");
        context.Move = null;
        stage.HideTargets();
        stage.ShowPickers();
        menu.ShowInterface(true);
        menu.ShowCancelButton(false);
        tooltips.HideAll();
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
        ApplyDecision(context.User.GetTurnDecision(context));
        PerformMove();
    }

    public void PerformAiCounter(Battler user)
    {
        Debug.Log($"Run Ai: {user}");
        ApplyDecision(context.User.GetCounterDecision(context));
        PerformMove();
    }

    private void PerformMove()
    {
        stage.HideTargets();
        menu.ShowInterface(false);
        menu.ShowCancelButton(false);
        tooltips.HideAll();
        positions.UpdateTeamPositions();
        positions.UpdateUserPosition();
        positions.UpdateTargetPositions();
        if (context.Move == null)
        {
            // Skip Turn
            // TODO: context.User.Actions--;
            fsm.Next();
        }
        else
        {
            // Start Animating
            hits = context.Move.logic.CreateHits(context);
            context.User.model.Animate(context.Move.animation);
        }
    }

    public void PerformFx()
    {
        context.User.effects.Animate(context.Move.fx);
    }

    public void PerformHit()
    {
        if (hits.Count <= 0)
        {
            Debug.LogError($"{nameof(BattleManager)}: No hits to perform");
            return;
        }
        hits[0].Execute(); // Try Execute to interrupt if foe is dead, Repeat flag for multihits
        hits.RemoveAt(0);
        positions.UpdateTargetPositions();
        info.UpdatePlayerInfos();
        info.UpdateNpcInfos();
    }

    public void NotifyIdle(GameObject obj)
    {
        var completedAnimations = animating.Remove(obj) && animating.Count <= 0;
        if (completedAnimations)
        {
            // TODO: context.User.Actions--;
            fsm.Next();
        }
    }

    public void NotifyBusy(GameObject obj)
    {
        animating.Add(obj);
    }
}
