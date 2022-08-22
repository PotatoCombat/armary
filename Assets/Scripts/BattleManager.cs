using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Runtime")]
    public BattleData data;
    public int wave;
    public List<Actor> animating;
    public BattleContext context;

    [Header("Components")]
    public BattleFsm fsm;
    public BattleStage stage;
    public BattleMenu menu;
    public BattleUi ui;

    private void OnEnable()
    {
        fsm.Load(this);
        fsm.Start();
    }

    // The following properties have no side-effects.

    public MoveData Move
    {
        get => context.move;
        set => context.move = value;
    }

    public Battler User
    {
        get => context.user;
        set => context.user = value;
    }

    public Battler TargetBattler
    {
        get => context.targetBattler;
        set => context.targetBattler = value;
    }

    public Team TargetTeam
    {
        get => context.targetTeam;
        set => context.targetTeam = value;
    }

    public Team AllyTeam
    {
        get => context.allyTeam;
        set => context.allyTeam = value;
    }

    public Team FoeTeam
    {
        get => context.foeTeam;
        set => context.foeTeam = value;
    }

    public Team PlayerTeam => stage.playerTeam;
    public Team NpcTeam => stage.npcTeam;
    public Team WeatherTeam => stage.weatherTeam;

    public bool SurpriseAttack => data.surpriseAttack;
    public bool AllPlayersDead => PlayerTeam.battlers.TrueForAll(battler => !battler.isAlive);
    public bool AllNpcsDead => NpcTeam.battlers.TrueForAll(battler => !battler.isAlive);
    public bool TurnEnded => AllyTeam.battlers.TrueForAll(battler => battler.actions == 0);
    public bool FinalWave => (wave + 1) == data.encounter.waves.Count;

    public void ShowPlayers()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    public void ShowNpcs()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    public void LoadWave(int wave)
    {
        if (wave < 0 || wave >= data.encounter.waves.Count)
        {
            return;
        }
        // stage.LoadNpcs(data.encounter.waves[wave].foes);
    }

    public void LoadPreviousWave()
    {
        LoadWave(--wave);
    }

    public void LoadNextWave()
    {
        LoadWave(++wave);
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
                ally.Load(battlerTemplate.CreateBattlerData());
                return;
            }
        }
    }

    // The following methods trigger UI side effects.

    public void ResetContext()
    {
        context = new BattleContext();
    }

    public void SelectUser(Battler user)
    {
        User = user;
        stage.LoadContext(AllyTeam, FoeTeam);
        stage.ShowPickers();
        User.ShowPicker(false);
        menu.LoadContext(User, AllyTeam);
        menu.SelectDefaultPanel();
        menu.ShowPanel(true);
        menu.ShowButtons(true);
        Debug.Log($"Selected User: {User}");
    }

    public void SelectMove(MoveData move)
    {
        Move = move;
        stage.HidePickers();
        stage.ShowTargets(User, Move.target);
        menu.HideTooltip();
        menu.ShowPanel(false);
        menu.ShowButtons(false);
        menu.ShowCancelButton(true);
        Debug.Log($"Selected Move: {Move.name}");
    }

    public void CancelMove()
    {
        Move = null;
        stage.HideTargets();
        stage.ShowPickers();
        User.ShowPicker(false);
        menu.ShowPanel(true);
        menu.ShowButtons(true);
        menu.ShowCancelButton(false);
        Debug.Log($"Cancelled move");
    }

    public void SelectTarget(Battler battler)
    {
        TargetBattler = battler;
        PerformMove();
        Debug.Log($"Target Battler: {battler}");
    }

    public void SelectTarget(Team team)
    {
        TargetTeam = team;
        PerformMove();
        Debug.Log($"Target Team: {team}");
    }

    private void PerformMove()
    {
        menu.ShowCancelButton(false);
        menu.ShowPanel(false);
        menu.ShowButtons(false);
        stage.HideTargets();
        User.model.Animate(Move.animation);
    }

    public void PerformAction(Actor actor, ActorEvent actorEvent)
    {
        Debug.Log($"{actor}, {actorEvent}");
        actorEvent.Invoke(actor, this);
    }

    public void NotifyIdleActor(Actor actor)
    {
        var completedAnimations = animating.Remove(actor) && animating.Count <= 0;
        if (completedAnimations)
        {
            fsm.Next();
        }
    }

    public void NotifyBusyActor(Actor actor)
    {
        animating.Add(actor);
    }
}
