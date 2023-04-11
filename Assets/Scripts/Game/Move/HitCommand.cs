using System;

[Serializable]
public abstract class HitCommand
{
    public abstract void Execute();
}

[Serializable]
public class BasicDamageCommand : HitCommand
{
    public Battler battler;
    public int value;

    public BasicDamageCommand(Battler battler, int value)
    {
        this.battler = battler;
        this.value = value;
    }

    public override void Execute()
    {
        battler.Damage(value);
    }
}
