using UnityEngine;

[CreateAssetMenu]
public class BattlerType : ScriptableObject
{
    public MoveData[] moves;
    // Base Stats

    public BattlerData CreateInstance()
    {
        return new BattlerData
        {
            moves = moves
        };
    }
}
