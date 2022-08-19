public class SimpleButton : CustomButton<SimpleButton>
{
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        Data = this;
    }

    protected override void Reset()
    {
        base.Reset();
        Data = this;
    }
#endif
}
