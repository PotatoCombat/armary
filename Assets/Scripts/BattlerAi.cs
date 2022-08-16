// // using UnityEngine;
// // using UnityEngine.UI;
// // using Random = UnityEngine.Random;
// //
//
// using UnityEditor;
// using UnityEngine;
//
// public class BattlerAi : MonoBehaviour
// {
//     // public abstract void TakeTurn(Battler user, BattleManager manager);
//
//     public void K()
//     {
//     }
// }
//
// [CustomEditor(typeof(BattlerAi))]
// public class BattlerAi_Editor : Editor
// {
//     public Object gameObject;
//
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         gameObject = EditorGUILayout.ObjectField(gameObject, typeof(Object), false);
//         GUILayout.Label(AssetPreview.GetAssetPreview(gameObject));
//     }
// }
//
// //
// // [CreateAssetMenu]
// // public class CactusAi : BattlerAi
// // {
// //     public override void TakeTurn(Battler user, BattleManager manager)
// //     {
// //         if (user.status[BERSERK])
// //         {
// //             manager.targets.Add(manager.RandomFoe());
// //             manager.move = moves["attack"];
// //         }
// //         else if (user.HpPercentage > 50)
// //         {
// //             manager.targets.Add(manager.RandomFoe());
// //             manager.move = moves["needle"];
// //         }
// //         else if (Random.value > 0.5f)
// //         {
// //             manager.targets.Add(user);
// //             manager.move = moves["heal"];
// //         }
// //         else
// //         {
// //             manager.targets.AddRange(manager.Foes);
// //             manager.move = moves["thousand-needles"];;
// //         }
// //         manager.PerformMove();
// //     }
// // }
// //
// // public class U : Image
// // {
// //
// // }
