using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public BattlePanel panel;

    [Header("Components")]
    public GameObject cancelButton;
    public BattlePanel listPanel;
    public BattlePanel defaultPanel;

    public void ShowInterface(bool visible)
    {
        listPanel.Load();
        listPanel.gameObject.SetActive(visible);
        if (panel)
        {
            panel.Load();
            panel.gameObject.SetActive(visible);
        }
    }

    public void ShowPanel(BattlePanel panel)
    {
        if (this.panel)
        {
            this.panel.gameObject.SetActive(false);
        }
        if (panel)
        {
            panel.Load();
            panel.gameObject.SetActive(true);
        }
        this.panel = panel;
    }

    public void ShowDefaultPanel()
    {
        ShowPanel(defaultPanel);
    }

    public void ShowCancelButton(bool visible)
    {
        cancelButton.gameObject.SetActive(visible);
    }
}
