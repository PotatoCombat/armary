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

    public Battler user;
    public Battler[] targets;

    public List<MoveButton> buttons;

    private void OnEnable()
    {
        LoadAllies();
        LoadFoes();
        LoadMenu();
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

    private void LoadMenu()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveListener(SelectMoveButton);
            button.onClick.AddListener(SelectMoveButton);
        }
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
                battlers[i].onClick.RemoveListener(SelectBattler);
                battlers[i].onClick.AddListener(SelectBattler);
            }
            else
            {
                battlers[i].gameObject.SetActive(false);
            }
        }
        return battlers;
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
}
