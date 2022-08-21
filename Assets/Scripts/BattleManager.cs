using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Runtime")]
    public BattleData data;

    public int wave;

    public Battler user;
    public MoveData move;
    public Battler targetBattler;
    public Team targetTeam;

    public List<Actor> animating;

    [Header("Components")]
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

    public bool SurpriseAttack => data.surpriseAttack;
    public bool AllPlayersDead => stage.playerTeam.battlers.TrueForAll(battler => !battler.isAlive);
    public bool AllNpcsDead => stage.npcTeam.battlers.TrueForAll(battler => !battler.isAlive);
    public bool TurnEnded => stage.allyTeam.battlers.TrueForAll(battler => battler.actions == 0);
    public bool FinalWave => (wave + 1) == data.encounter.waves.Count;

    public void ShowPlayers()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    public void HidePlayers()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    public void ShowNpcs()
    {
        // stage.LoadPlayers(data.party.allies);
    }

    public void HideNpcs()
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

    public void ShowAttacks()
    {
        menu.ShowAttacks();
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

    public void SelectUser(Battler user)
    {
        this.user = user;
        menu.Show(user, stage.allyTeam);
        menu.ShowHistory();
        stage.ShowPickers(stage.allyTeam);
        user.ShowPicker(false);
        Debug.Log($"Selected User: {user}");
    }

    public void SelectMove(MoveData move)
    {
        this.move = move;
        // menu.moveTooltip.Hide();
        menu.Hide();
        stage.HidePickers();
        stage.ShowTargets(user, move.target);
        Debug.Log($"Selected Move: {move.name}");
    }

    public void CancelMove()
    {
        this.move = null;
        menu.Show(user, stage.allyTeam);
        stage.HideTargets();
        stage.ShowPickers(stage.allyTeam);
        user.ShowPicker(false);
        Debug.Log($"Cancelled move");
    }

    public void TargetBattler(Battler battler)
    {
        this.targetBattler = battler;
        PerformMove();
        Debug.Log($"Target Battler: {battler}");
    }

    public void TargetTeam(Team team)
    {
        this.targetTeam = team;
        PerformMove();
        Debug.Log($"Target Team: {team}");
    }

    private void PerformMove()
    {
        menu.Hide();
        stage.HideTargets();
        user.model.Animate(move.animation);
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
