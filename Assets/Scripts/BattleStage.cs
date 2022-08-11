// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// [Serializable]
// public class BattleStage
// {
//     public Transform root;
//     public List<Battler> players;
//     public List<Battler> npcs;
//     public Battler weather;
//
//     private void OnEnable()
//     {
//         // LoadMenu();
//         // LoadStage();
//         LoadVariables();
//         LoadParty();
//         // LoadWave();
//     }
//
//     private void LoadVariables()
//     {
//         wave = 0;
//         turn = 0;
//         state = BattleState.Start;
//         battlerDB.Load();
//         effectsDB.Load();
//     }
//
//     public void LoadParty()
//     {
//         var allies = data.party.allies;
//         for (var i = 0; i < players.Count; i++)
//         {
//             if (i < allies.Count)
//             {
//                 players[i].Load(allies[i]);
//                 players[i].transform.localPosition = positions.GetAllyPosition(allies.Count, i);
//             }
//             else
//             {
//                 players[i].Hide();
//             }
//         }
//     }
//
//     public void LoadWave(int wave)
//     {
//         if (wave < 0 || wave >= data.encounter.waves.Count)
//         {
//             return;
//         }
//         var foes = data.encounter.waves[wave].foes;
//         for (var i = 0; i < npcs.Count; i++)
//         {
//             if (i < foes.Count)
//             {
//                 npcs[i].Load(foes[i].CreateBattlerData());
//                 npcs[i].transform.localPosition = foes[i].position;
//             }
//             else
//             {
//                 npcs[i].Hide();
//             }
//         }
//     }
//
//     public void LoadPreviousWave()
//     {
//         LoadWave(--wave);
//     }
//
//     public void LoadNextWave()
//     {
//         LoadWave(++wave);
//     }
//
//     // public void SwapBattler(int index, BattlerData battlerData)
//     // {
//     //     if (currentAllies.Length < index)
//     //     {
//     //         currentAllies[index].Load(battlerData);
//     //     }
//     // }
//
//     public void SpawnBattler(BattlerTemplate battlerTemplate)
//     {
//         foreach (var ally in GetAllies())
//         {
//             if (ally.data == null)
//             {
//                 ally.Load(battlerTemplate.CreateBattlerData());
//                 return;
//             }
//         }
//     }
//
//     public List<Battler> GetAllies()
//     {
//         return state switch
//         {
//             BattleState.Player => players,
//             BattleState.Npc => npcs,
//             _ => new List<Battler>(),
//         };
//     }
//
//     public List<Battler> GetFoes()
//     {
//         return state switch
//         {
//             BattleState.Player => npcs,
//             BattleState.Npc => players,
//             _ => new List<Battler>(),
//         };
//     }
//
//     public List<Battler> GetBattlers()
//     {
//         // Make private
//         var battlers = new List<Battler>();
//         battlers.AddRange(players);
//         battlers.AddRange(npcs);
//         return battlers;
//     }
//
//     public void StartPlayerTurn()
//     {
//         state = BattleState.Player;
//     }
//
//     public void StartNpcTurn()
//     {
//         state = BattleState.Npc;
//     }
//
//     public void StartWeatherTurn()
//     {
//         state = BattleState.Weather;
//     }
//
//     public void EndTurn()
//     {
//         move = null;
//         user = null;
//         targets = null;
//         targetIndex = 0;
//
//         // if (players.dead) => state = BattleState.Gameover;
//         // if (npcs.dead) => state = BattleState.Victory;
//         if (state == BattleState.Player)
//         {
//             //if (players.hasActions)
//             StartPlayerTurn();
//             //else
//             StartNpcTurn();
//         }
//         else if (state == BattleState.Npc)
//         {
//             //if (npcs.hasActions)
//             StartNpcTurn();
//             //else
//             StartWeatherTurn();
//         }
//         else if (state == BattleState.Weather)
//         {
//             //if (npcs.hasActions)
//             StartWeatherTurn();
//             //else
//             StartPlayerTurn();
//         }
//     }
//
//     public void ShowMoves(Battler battler)
//     {
//         var moves = battler.data.moves;
//         for (var i = 0; i < buttons.Count; i++)
//         {
//             if (i < moves.Length)
//             {
//                 buttons[i].SetData(moves[i]);
//             }
//             else
//             {
//                 buttons[i].ClearData();
//             }
//         }
//     }
//
//     public void SelectMove(MoveButton moveButton)
//     {
//         Debug.Log($"Selected: {moveButton}");
//         move = moveButton.data;
//         // ShowTargets();
//     }
//
//     public void CancelMove()
//     {
//         Debug.Log($"Cancelled move");
//         move = null;
//         // HideTargets();
//         // ShowMoves();
//     }
//
//     public void SelectUser(Battler battler)
//     {
//         Debug.Log($"Selected: {battler}");
//         user = battler;
//         // ShowMoves();
//     }
//
//     public void SelectTargets(List<Battler> battlers)
//     {
//         var debug = "Selected: ";
//         foreach (var battler in battlers)
//         {
//             debug += $"{battler}, ";
//         }
//         Debug.Log(debug);
//         targets = battlers;
//         // PerformMove();
//     }
//
//     public void PerformMove()
//     {
//         if (move && user && targets != null)
//         {
//             // var hit = move.power * user.damage;
//             // if (move.target == single)
//             // {
//             //     targets[].Hit(hit);
//             // }
//         }
//     }
//
//     public void PerformHit()
//     {
//         // var hit = move.power * user.damage;
//         // if (move.target == single)
//         // {
//         //     targets[].Hit(hit);
//         // }
//     }
//
//     public void PlayWeaponEffect()
//     {
//         // PlayEffect(user.weapon.effect, move.target);
//     }
//
//     public void PlayMoveEffect()
//     {
//         PlayEffect(move.effect, move.target);
//     }
//
//     public void PlayEffect(string name, string targetting)
//     {
//         var effect = Instantiate(effectsDB[name], stage);
//         effect.actor.onAnimationEvent.AddListener(HandleAnimationEvent);
//         effect.actor.Busy();
//     }
//
//     public void HandleAnimationEvent(Actor actor, ActorEvent evt)
//     {
//         Debug.Log($"Animation Event ({evt}): {actor}");
//         switch (evt)
//         {
//             case ActorEvent.Idle:
//                 var isIdle = animating.Remove(actor) && animating.Count <= 0;
//                 if (isIdle)
//                 {
//                     EndTurn();
//                 }
//                 break;
//             case ActorEvent.Busy:
//                 animating.Add(actor);
//                 break;
//             case ActorEvent.Hit:
//                 PerformHit();
//                 PlayWeaponEffect();
//                 break;
//             case ActorEvent.Effect:
//                 PlayMoveEffect();
//                 break;
//         }
//     }
// }
