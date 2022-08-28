using System;

[Serializable]
public abstract class HitCommand
{
    public abstract void Execute();
}

[Serializable]
public class BasicHitCommand : HitCommand
{
    public float value;

    public BasicHitCommand(float value)
    {
        this.value = value;
    }

    public override void Execute()
    {

    }
}
