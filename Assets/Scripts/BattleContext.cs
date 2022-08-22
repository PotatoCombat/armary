using System;

[Serializable]
public class BattleContext
{
    public Team allyTeam;
    public Team foeTeam;
    public Battler user;
    public MoveData move;
    public Battler targetBattler;
    public Team targetTeam;
}
