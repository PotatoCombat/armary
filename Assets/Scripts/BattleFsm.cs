using System.Collections.Generic;
using UnityEngine;

public class BattleFsm : MonoBehaviour
{
    [SerializeField]
    private State state;
    private Dictionary<State, Transition>  _transitions;

    public enum State
    {
        Start,
        Wave,
        Player,
        Npc,
        Weather,
        Victory,
        Gameover,
        Flee,
        End,
    }

    public void Load(BattleManager manager)
    {
        _transitions = new Dictionary<State, Transition>
        {
            { State.Start, new StartBattle(this, manager) },
            { State.Wave, new LoadWave(this, manager) },
            { State.Player, new PlayerTurn(this, manager) },
            { State.Npc, new NpcTurn(this, manager) },
            { State.Weather, new WeatherTurn(this, manager) },
            { State.Victory, new Victory(this, manager) },
            { State.Gameover, new Gameover(this, manager) },
            { State.Flee, new FleeBattle(this, manager) },
            { State.End, new EndBattle(this, manager) },
        };
    }

    public void Start()
    {
        ChangeState(State.Start);
        Next();
        Next();
    }

    public void ChangeState(State state)
    {
        this.state = state;
        _transitions[this.state].Enter();
    }

    public void Next()
    {
        _transitions[this.state].Next();
    }

    private abstract class Transition
    {
        protected BattleFsm Fsm;
        protected BattleManager Manager;

        protected Transition(BattleFsm fsm, BattleManager manager)
        {
            Fsm = fsm;
            Manager = manager;
        }

        public virtual void Enter()
        {
            // Debug.Log($"State: {GetType().Name}");
            Manager.ResetContext();
        }

        public virtual void Next()
        {

        }
    }

    private class StartBattle : Transition
    {
        public StartBattle(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.LoadParty();
            Manager.ShowPlayers();
        }

        public override void Next()
        {
            base.Next();
            Fsm.ChangeState(State.Wave);
        }
    }

    private class LoadWave : Transition
    {
        public LoadWave(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.LoadNextWave();
            Manager.ShowNpcs();
        }

        public override void Next()
        {
            base.Next();
            Fsm.ChangeState(Manager.SurpriseAttack ? State.Npc : State.Player);
        }
    }

    private class Victory : Transition
    {
        public Victory(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
            Fsm.ChangeState(State.End);
        }
    }

    private class Gameover : Transition
    {
        public Gameover(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
            Fsm.ChangeState(State.End);
        }
    }

    private class FleeBattle : Transition
    {
        public FleeBattle(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
        }
    }

    private class EndBattle : Transition
    {
        public EndBattle(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
        }
    }

    private class PlayerTurn : Turn
    {
        public PlayerTurn(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        protected override Team AllyTeam => Manager.PlayerTeam;
        protected override Team FoeTeam => Manager.NpcTeam;
        protected override State CurrentTurn => State.Player;
        protected override State NextTurn => State.Npc;
    }

    private class NpcTurn : Turn
    {
        public NpcTurn(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        protected override Team AllyTeam => Manager.NpcTeam;
        protected override Team FoeTeam => Manager.PlayerTeam;
        protected override State CurrentTurn => State.Npc;
        protected override State NextTurn => State.Player;
    }

    private class WeatherTurn : Turn
    {
        public WeatherTurn(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        protected override Team AllyTeam => Manager.WeatherTeam;
        protected override Team FoeTeam => Manager.WeatherTeam;
        protected override State CurrentTurn => State.Weather;
        protected override State NextTurn => State.Player;
    }

    private abstract class Turn : Transition
    {
        protected Turn(BattleFsm fsm, BattleManager manager) : base(fsm, manager) { }

        protected abstract Team AllyTeam { get; }
        protected abstract Team FoeTeam { get; }
        protected abstract State CurrentTurn { get; }
        protected abstract State NextTurn { get; }

        public override void Enter()
        {
            base.Enter();
            Manager.AllyTeam = AllyTeam;
            Manager.FoeTeam = FoeTeam;
            Manager.SelectUser(Manager.AllyTeam.battlers.Find(battler => battler.actions == 0));
        }

        public override void Next()
        {
            base.Next();
            if (Manager.AllPlayersDead)
            {
                Fsm.ChangeState(State.Gameover);
            }
            else if (Manager.FinalWave && Manager.AllNpcsDead)
            {
                Fsm.ChangeState(State.Victory);
            }
            else if (Manager.AllNpcsDead)
            {
                Fsm.ChangeState(State.Wave);
            }
            else if (Manager.TurnEnded)
            {
                Fsm.ChangeState(NextTurn);
            }
            else
            {
                Fsm.ChangeState(CurrentTurn);
            }
        }
    }
}
