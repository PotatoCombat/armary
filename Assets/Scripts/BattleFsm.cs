using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleFsm
{
    [SerializeField]
    private State state;
    private Dictionary<State, Transition>  _transitions;

    public void Load(BattleManager manager)
    {
        _transitions = new Dictionary<State, Transition>
        {
            { State.Start, new StartBattle(manager) },
            { State.Wave, new LoadWave(manager) },
            { State.Player, new PlayerTurn(manager) },
            { State.Npc, new NpcTurn(manager) },
            { State.Weather, new WeatherTurn(manager) },
            { State.Victory, new Victory(manager) },
            { State.Gameover, new Gameover(manager) },
            { State.End, new EndBattle(manager) },
        };
    }

    public void Start()
    {
        ChangeState(State.Start);
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

    public enum State
    {
        Start,
        Wave,
        Player,
        Npc,
        Weather,
        Victory,
        Gameover,
        End,
    }

    public abstract class Transition
    {
        protected BattleManager Manager;
        protected Transition(BattleManager manager)
        {
            Manager = manager;
        }

        public virtual void Enter()
        {
            // Debug.Log($"State: {GetType().Name}");
            Manager.move = null;
            Manager.user = null;
            Manager.targets = null;
            Manager.targetIndex = 0;
        }

        public virtual void Next()
        {

        }
    }

    public class StartBattle : Transition
    {
        public StartBattle(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.wave = 0;
            Manager.animating = new List<Actor>();
            Manager.Battlers = new List<Battler>();
            Manager.Battlers.AddRange(Manager.stage.players);
            Manager.Battlers.AddRange(Manager.stage.npcs);
        }

        public override void Next()
        {
            base.Next();
            Manager.fsm.ChangeState(State.Wave);
        }
    }

    public class LoadWave : Transition
    {
        public LoadWave(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
            if (Manager.wave == 0)
            {
                Manager.ShowPlayers();
            }
            Manager.LoadNextWave();
        }

        public override void Next()
        {
            base.Next();
            Manager.fsm.ChangeState(Manager.SurpriseAttack ? State.Npc : State.Player);
        }
    }

    public class PlayerTurn : Transition
    {
        public PlayerTurn(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.Allies = Manager.stage.players;
            Manager.Foes = Manager.stage.npcs;
        }

        public override void Next()
        {
            base.Next();
            if (Manager.AllPlayersDead)
            {
                Manager.fsm.ChangeState(State.Gameover);
            }
            else if (Manager.FinalWave && Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Victory);
            }
            else if (Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Wave);
            }
            else if (Manager.TurnEnded)
            {
                Manager.fsm.ChangeState(State.Npc);
            }
            else
            {
                Manager.fsm.ChangeState(State.Player);
            }
        }
    }

    public class NpcTurn : Transition
    {
        public NpcTurn(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.Allies = Manager.stage.npcs;
            Manager.Foes = Manager.stage.players;
        }

        public override void Next()
        {
            base.Next();
            if (Manager.AllPlayersDead)
            {
                Manager.fsm.ChangeState(State.Gameover);
            }
            else if (Manager.FinalWave && Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Victory);
            }
            else if (Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Wave);
            }
            else if (Manager.TurnEnded)
            {
                Manager.fsm.ChangeState(State.Weather);
            }
            else
            {
                Manager.fsm.ChangeState(State.Npc);
            }
        }
    }

    public class WeatherTurn : Transition
    {
        public WeatherTurn(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
            Manager.Allies = new List<Battler>(); //manager.stage.weather);
            Manager.Foes = new List<Battler>(); //manager.stage.weather);
        }

        public override void Next()
        {
            base.Next();
            if (Manager.AllPlayersDead)
            {
                Manager.fsm.ChangeState(State.Gameover);
            }
            else if (Manager.FinalWave && Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Victory);
            }
            else if (Manager.AllNpcsDead)
            {
                Manager.fsm.ChangeState(State.Wave);
            }
            else if (Manager.TurnEnded)
            {
                Manager.fsm.ChangeState(State.Player);
            }
            else
            {
                Manager.fsm.ChangeState(State.Weather);
            }
        }
    }

    public class Victory : Transition
    {
        public Victory(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
            Manager.fsm.ChangeState(State.End);
        }
    }

    public class Gameover : Transition
    {
        public Gameover(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
            Manager.fsm.ChangeState(State.End);
        }
    }

    public class EndBattle : Transition
    {
        public EndBattle(BattleManager manager) : base(manager) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Next()
        {
            base.Next();
        }
    }
}
