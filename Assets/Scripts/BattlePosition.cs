using UnityEngine;

public class BattlePosition : MonoBehaviour
{
    [SerializeField] private BattleContext context;

    [Header("Positions")]
    [SerializeField] private Vector3Value allyTeam;
    [SerializeField] private Vector3Value foeTeam;
    [SerializeField] private Vector3Value user;
    [SerializeField] private Vector3Value targetBattler;
    [SerializeField] private Vector3Value targetTeam;

    public void UpdateTeamPositions()
    {
        allyTeam.Value = context.AllyTeam.transform.position;
        foeTeam.Value = context.FoeTeam.transform.position;
    }

    public void UpdateUserPosition()
    {
        user.Value = context.User.transform.position;
    }

    public void UpdateTargetPositions()
    {
        if (context.TargetBattler)
        {
            targetBattler.Value = context.TargetBattler.transform.position;
        }
        if (context.TargetTeam)
        {
            targetTeam.Value = context.TargetTeam.transform.position;
        }
    }
}
