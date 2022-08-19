public class MoveButton : CustomButton<MoveData>
{
    protected override void SetData(MoveData data)
    {
        base.SetData(data);
        targetImage.sprite = data.sprite;
    }
}
