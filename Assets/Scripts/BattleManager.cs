using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] private BattleData data;
    [SerializeField] private int wave;
    [SerializeField] private List<Actor> animating;
    [SerializeField] private BattleContext context;

    [Header("Components")]
    [SerializeField] private BattleStage stage;
    [SerializeField] private BattleInfo info;
    [SerializeField] private BattleMenu menu;
    [SerializeField] private BattleFsm fsm;

    private void Start()
    {
        StartBattle();
    }

    // The following properties have no side-effects.

    public int Wave
    {
        get => wave;
        private set => wave = value;
    }

    public MoveType Move
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
    public bool AllPlayersDead => PlayerTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool AllNpcsDead => NpcTeam.battlers.TrueForAll(battler => !battler.IsAlive);
    public bool TurnEnded => AllyTeam.battlers.TrueForAll(battler => battler.actions == 0);
    public bool FinalWave => (wave + 1) == data.encounter.waves.Count;

    public void LoadParty()
    {
        PlayerTeam.Load(data.party);
        for (var i = 0; i < PlayerTeam.battlers.Count; i++)
        {
            var battler = PlayerTeam.battlers[i];
            var healthBar = info.players[i];
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

    public void LoadWave(int wave)
    {
        if (wave < 0 || wave >= data.encounter.waves.Count)
        {
            return;
        }
        Wave = wave;
        NpcTeam.Load(data.encounter.waves[wave]);
        for (var i = 0; i < NpcTeam.battlers.Count; i++)
        {
            var battler = NpcTeam.battlers[i];
            var healthBar = info.npcs[i];
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
        wave = -1;
        animating = new List<Actor>();
        fsm.Load(this);
        fsm.Init();
    }

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
        menu.Show();
        Debug.Log($"Selected User: {User}");
    }

    public void SelectMove(MoveType move)
    {
        Move = move;
        stage.HidePickers();
        stage.ShowTargets(User, Move.target);
        Debug.Log($"Selected Move: {Move.name}");
    }

    public void CancelMove()
    {
        Move = null;
        stage.HideTargets();
        stage.ShowPickers();
        User.ShowPicker(false);
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
        stage.HideTargets();
        menu.Hide();
        User.model.Animate(Move.animation);
    }

    public void PerformAction(Actor actor, ActorEvent actorEvent)
    {
        Debug.Log($"Perform {actorEvent}: {actor}");
        actorEvent.Invoke(actor, this);
    }

    public void PerformHit(Battler battler, HitEvent hitEvent)
    {
        Debug.Log($"Perform {hitEvent}: {battler}");
        hitEvent.Invoke(battler, this);
    }

    public void PerformHit(Team team, HitEvent hitEvent)
    {
        Debug.Log($"Perform {hitEvent}: {team}");
        hitEvent.Invoke(team, this);
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
