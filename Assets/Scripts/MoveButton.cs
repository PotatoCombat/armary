using TMPro;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public MoveData data;

    public void SetData(MoveData data)
    {
        this.data = data;
        text.text = data.name;
    }

    public void ClearData()
    {
        this.data = null;
        text.text = "";
    }
}
