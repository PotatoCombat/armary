using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI text;
    public MoveData data;

    public UnityEvent<MoveButton> onClick;

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

    public void Click()
    {
        onClick.Invoke(this);
    }
}
