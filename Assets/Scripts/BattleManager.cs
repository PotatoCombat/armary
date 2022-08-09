using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public BattleData data;

    public Formations formations;
    public Formation allyFormation;
    public Formation foeFormation;

    public Battler[] allies;
    public Battler[] foes;
    public Battler[] weather;

    public int turn;
    public List<Battler[]> turnOrder;

    public Battler user;
    public Battler[] targets;

    public List<MoveButton> buttons;
    public BattleFsm fsm;

    public List<Battler> animating;
    public List<Action> animations;

    public bool next;

    private void OnEnable()
    {
        LoadMenu();
        LoadAllies();
        LoadFoes();
        LoadTurns();
        LoadAnimations();
        LoadFsm();
    }

    private void LoadMenu()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveListener(SelectMoveButton);
            button.onClick.AddListener(SelectMoveButton);
        }
    }

    private void LoadAllies()
    {
        if (allyFormation)
        {
            allyFormation.gameObject.SetActive(false);
        }
        allyFormation = formations.GetAllyFormation(data.allies.Count);
        if (allyFormation)
        {
            allyFormation.gameObject.SetActive(true);
        }
        allies = LoadBattlers(allyFormation, data.allies);
    }

    private void LoadFoes()
    {
        if (foeFormation)
        {
            foeFormation.gameObject.SetActive(false);
        }
        foeFormation = formations.GetFoeFormation(data.foeFormation);
        if (foeFormation)
        {
            foeFormation.gameObject.SetActive(true);
        }
        foes = LoadBattlers(foeFormation, data.foes);
    }

    private void LoadTurns()
    {
        turn = 0;
        turnOrder = new List<Battler[]>
        {
            allies,
            foes,
            weather,
        };
    }

    private void LoadAnimations()
    {
        animations = new List<Action>();
    }

    private void LoadFsm()
    {
        fsm.SetController(this);
        fsm.ChangeState(new StartBattleState());
    }

    private Battler[] LoadBattlers(Formation formation, List<BattlerData> battlerDatas)
    {
        if (!formation)
        {
            return Array.Empty<Battler>();
        }
        var battlers = formation.battlers;
        for (var i = 0; i < battlers.Length; i++)
        {
            if (i < battlerDatas.Count)
            {
                battlers[i].gameObject.SetActive(true);
                battlers[i].SetData(battlerDatas[i]);
            }
            else
            {
                battlers[i].gameObject.SetActive(false);
            }
            battlers[i].onClick.RemoveListener(SelectBattler);
            battlers[i].onClick.AddListener(SelectBattler);
            battlers[i].onAnimationEvent.RemoveListener(HandleAnimationEvent);
            battlers[i].onAnimationEvent.AddListener(HandleAnimationEvent);
        }
        return battlers;
    }

    private void Update()
    {
        if (next)
        {
            next = false;
            user = allies[0];
            targets = new []
            {
                foes[0],
            };
            new MoveLogic().Play(this);
            StartAnimations();
        }
    }

    public Battler[] GetCurrentTurn()
    {
        return turnOrder[(turn - 1) % turnOrder.Count];
    }

    public Battler[] GetNextTurn()
    {
        return turnOrder[turn % turnOrder.Count];
    }

    public void StartAnimations()
    {
        fsm.ChangeState(new AnimatingState());
    }

    public void FinishAnimations()
    {
        var nextTurn = GetNextTurn();
        if (nextTurn == foes)
        {
            fsm.ChangeState(new FoeTurnState());
        }
        else if (nextTurn == weather)
        {
            fsm.ChangeState(new WeatherTurnState());
        }
        else
        {
            fsm.ChangeState(new AllyTurnState());
        }
    }

    public void QueueAnimations(params Action[] anims)
    {
        animations.AddRange(anims);
    }

    public void PlayNextAnimation()
    {
        if (animations.Count > 0)
        {
            animations[0].Invoke();
            animations.RemoveAt(0);
        }
    }

    public void ShowMoves(Battler battler)
    {
        var moves = battler.data.moves;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < moves.Length)
            {
                buttons[i].SetData(moves[i]);
            }
            else
            {
                buttons[i].ClearData();
            }
        }
    }

    public void SelectMoveButton(MoveButton moveButton)
    {
        Debug.Log($"Selected: {moveButton}");
        if (user)
        {
            user.PlayAnimation(moveButton.data.animation);
        }
    }

    public void SelectBattler(Battler battler)
    {
        Debug.Log($"Selected: {battler}");
        user = battler;
        ShowMoves(user);
    }

    public void HandleAnimationEvent(Battler battler, BattlerAnimationEvent evt)
    {
        Debug.Log($"Animation Event ({evt}): {battler}");
        switch (evt)
        {
            case BattlerAnimationEvent.Idle:
                var isIdle = animating.Remove(battler) && animating.Count <= 0;
                if (isIdle)
                {
                    if (animations.Count > 0)
                    {
                        PlayNextAnimation();
                    }
                    else
                    {
                        FinishAnimations();
                    }
                }
                break;
            case BattlerAnimationEvent.Busy:
                animating.Add(battler);
                break;
            case BattlerAnimationEvent.Act:
                if (animations.Count > 0)
                {
                    PlayNextAnimation();
                }
                break;
        }
    }
}
