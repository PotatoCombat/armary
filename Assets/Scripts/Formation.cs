using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class Formation : MonoBehaviour
{
    public FormationType type;
    public Battler[] battlers;

    [ContextMenu("Load")]
    public void Load()
    {
        var updated = false;
        var newBattlers = GetComponentsInChildren<Battler>();
        if (!newBattlers.SequenceEqual(battlers))
        {
            battlers = newBattlers;
            updated = true;
        }
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            updated = true;
        }
#if UNITY_EDITOR
        if (updated)
        {
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
#endif
    }
}
