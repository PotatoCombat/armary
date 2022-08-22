using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public BattleData data;

    public int wave;
    public List<Actor> animating;

    public BattleContext context;
    public BattleFsm fsm;
    public BattleStage stage;
    public BattleMenu menu;
    public BattleUi ui;

    // Accessors

    private void OnEnable()
    {
        fsm.Load(this);
        fsm.Start();
    }

    public MoveData Move => context.move;

    public Battler User => context.user;
    public Battler TargetBattler => context.targetBattler;

    public Team PlayerTeam => stage.playerTeam;
    public Team NpcTeam => stage.npcTeam;
    public Team WeatherTeam => stage.weatherTeam;
    public Team AllyTeam => context.allyTeam;
    public Team FoeTeam => context.foeTeam;
    public Team TargetTeam => context.targetTeam;

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

    public void LoadContext(BattleContext context)
    {
        this.context = context;
    }

    public void SelectUser(Battler user)
    {
        context.user = user;
        menu.LoadContext(context.user, context.allyTeam);
        menu.Show(true);
        stage.ShowPickers();
        context.user.ShowPicker(false);
        Debug.Log($"Selected User: {context.user}");
    }

    public void SelectMove(MoveData move)
    {
        context.move = move;
        // menu.moveTooltip.Hide();
        menu.Show(false);
        stage.HidePickers();
        stage.ShowTargets(context.user, context.move.target);
        Debug.Log($"Selected Move: {context.move.name}");
    }

    public void CancelMove()
    {
        context.move = null;
        menu.Show(true);
        stage.HideTargets();
        stage.ShowPickers();
        context.user.ShowPicker(false);
        Debug.Log($"Cancelled move");
    }

    public void SelectTarget(Battler battler)
    {
        context.targetBattler = battler;
        PerformMove();
        Debug.Log($"Target Battler: {battler}");
    }

    public void SelectTarget(Team team)
    {
        context.targetTeam = team;
        PerformMove();
        Debug.Log($"Target Team: {team}");
    }

    private void PerformMove()
    {
        // menu.Hide();
        stage.HideTargets();
        context.user.model.Animate(context.move.animation);
        // var hit = move.power * user.damage;
        // if (move.target == single)
        // {
        //     targets[].Hit(hit);
        // }
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
