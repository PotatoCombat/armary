using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Runtime")]
    public BattleData data;

    public int wave;

    public MoveData move;
    public Battler user;

    public int targetIndex;
    public List<Battler> targets;

    public List<Actor> animating;

    [Header("Components")]
    public BattleFsm fsm;
    public BattleStage stage;
    public BattleUi ui;

    // Accessors
    public List<Battler> Allies { get; set; }
    public List<Battler> Foes { get; set; }
    public List<Battler> Battlers { get; set; }

    private void Start()
    {
        // Runs after enable
        // battlerDB.Load();
        // effectsDB.Load();
    }

    private void OnEnable()
    {
        // fsm.Load(this);
        // fsm.Start();
        stage.ShowPickers(Faction.Player);
    }

    public bool SurpriseAttack => data.surpriseAttack;
    public bool AllPlayersDead => stage.players.TrueForAll(battler => !battler.isAlive);
    public bool AllNpcsDead => stage.npcs.TrueForAll(battler => !battler.isAlive);
    public bool TurnEnded => Allies.TrueForAll(battler => battler.Actions == 0);
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
        ui.ShowAttacks();
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
        foreach (var ally in Allies)
        {
            if (ally.data == null)
            {
                ally.Load(battlerTemplate.CreateBattlerData());
                return;
            }
        }
    }

    public void ShowMenu()
    {
        ui.menu.Show();
        // If user is numb
        // If user is silent
    }

    public void ShowTargets()
    {
        stage.ShowTargets(user, move.target);
    }

    public void SelectMove(int index)
    {
        move = user.data.moves[index];
        ui.moveTooltip.Hide();
        ui.lastPanel.Hide();
        ui.menu.Hide();
        stage.ShowTargets(user, move.target);
        Debug.Log($"Selected Move: {move.name}");
    }

    public void CancelMove()
    {
        move = null;
        stage.HideTargets();
        ui.lastPanel.Show();
        ui.menu.Show();
        Debug.Log($"Cancelled move");
    }

    public void SelectUser(Battler battler)
    {
        Debug.Log($"Selected User: {battler}");
        user = battler;
        // ShowMoves();
    }

    public void SelectTarget(Battler battler)
    {
        SelectTargets(new List<Battler>(){battler});
    }

    public void SelectTargets(List<Battler> battlers)
    {
        targets = battlers;
        var debug = "Selected Targets: ";
        foreach (var battler in battlers)
        {
            debug += $"{battler}, ";
        }
        Debug.Log(debug);
        // PerformMove();
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

    public void PerformMove()
    {
        if (move && user && targets != null)
        {
            // var hit = move.power * user.damage;
            // if (move.target == single)
            // {
            //     targets[].Hit(hit);
            // }
        }
    }

    public void PerformHit()
    {
        // var hit = move.power * user.damage;
        // if (move.target == single)
        // {
        //     targets[].Hit(hit);
        // }
    }

    public void PlayWeaponEffect()
    {
        // PlayEffect(user.weapon.effect, move.target);
    }

    public void PlayMoveEffect()
    {
        // PlayEffect(move.effect, move.target);
    }

    public void PlayEffect(string name, string targetting)
    {
        // var effect = Instantiate(effectsDB[name], stage.transform);
        // effect.actor.onAnimationEvent.AddListener(HandleAnimationEvent);
        // effect.actor.Busy();
    }

    public void HandleAnimationEvent(Actor actor, ActorEvent evt)
    {
        Debug.Log($"Animation Event ({evt}): {actor}");
        switch (evt)
        {
            case ActorEvent.Idle:
                NotifyIdleActor(actor);
                break;
            case ActorEvent.Busy:
                NotifyBusyActor(actor);
                break;
            case ActorEvent.Hit:
                PerformHit();
                PlayWeaponEffect();
                break;
            case ActorEvent.Effect:
                PlayMoveEffect();
                break;
        }
    }
}
