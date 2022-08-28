using UnityEngine;

public class BattlePosition : MonoBehaviour
{
    [SerializeField] private Vector3Value allyTeam;
    [SerializeField] private Vector3Value foeTeam;
    [SerializeField] private Vector3Value user;
    [SerializeField] private Vector3Value targetBattler;
    [SerializeField] private Vector3Value targetTeam;

    // TODO: Check if Start works
    private void Awake()
    {
        User = Vector3.one;
    }

    public Vector3 AllyTeam
    {
        get => allyTeam.Value;
        set => allyTeam.Value = value;
    }

    public Vector3 FoeTeam
    {
        get => foeTeam.Value;
        set => foeTeam.Value = value;
    }

    public Vector3 User
    {
        get => user.Value;
        set => user.Value = value;
    }

    public Vector3 TargetBattler
    {
        get => targetBattler.Value;
        set => targetBattler.Value = value;
    }

    public Vector3 TargetTeam
    {
        get => targetTeam.Value;
        set => targetTeam.Value = value;
    }
}
