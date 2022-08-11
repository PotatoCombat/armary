// using System;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using Random = UnityEngine.Random;
//
// public class Ai : Battler
// {
//     public void TakeTurn(BattleManager controller)
//     {
//         controller.user = this;
//         if (this.status.BERSERK)
//         {
//             controller.targets.Add(controller.RandomFoe());
//             controller.move = Attack;
//         }
//         else if (hp * 100 / maxHp > 10)
//         {
//             controller.targets.Add(controller.RandomFoe());
//             controller.move = Needle;
//         }
//         else
//         {
//             controller.targets.AddRange(controller.GetOpposingBattlers());
//             controller.move = ThousandNeedles;
//         }
//         controller.EndTurn();
//     }
//
//     public void Needle(BattleManager controller)
//     {
//         controller.QueueAnimations(
//             controller.user.Attack1(),
//             controller.target.Hit(Random.Range(-100, 100))
//         );
//     }
//
//     public void ThousandNeedles(BattleManager controller)
//     {
//         controller.QueueAnimations(
//             controller.user.Attack1(),
//             controller.target.Hurt(1000)
//         );
//     }
//
//     public void Heal(BattleManager controller)
//     {
//         var target = controller.RandomFoe();
//         controller.QueueAnimations(
//             controller.user.Magic1(),
//             controller.effects.Heal(),
//             () =>
//             {
//                 foreach (var ally in controller.allies)
//                 {
//                     ally.Hit(1000);
//                 }
//             }
//         );
//     }
// }
