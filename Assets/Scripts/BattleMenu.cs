using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    [Header("Runtime")]
    public GameObject panel;

    [Header("Components")]
    public GameObject cancelButton;
    public GameObject listPanel;
    public GameObject defaultPanel;

    public void ShowInterface(bool visible)
    {
        listPanel.gameObject.SetActive(visible);
        if (panel)
        {
            panel.gameObject.SetActive(visible);
        }
    }

    public void ShowPanel(GameObject panel)
    {
        if (this.panel)
        {
            this.panel.SetActive(false);
        }
        if (panel)
        {
            panel.SetActive(true);
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
