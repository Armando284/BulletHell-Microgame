using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    [SerializeField] private GameObject gfxPanel;
    [SerializeField] private PanelUIName panelUIName;
    public bool isPermanent = false;
    private bool isVisible = true;

    // Callback which is triggered when an panel is opened.
    public delegate void OnPanelOpened();
    public OnPanelOpened onPanelOpenedCallback;

    public PanelUIName GetPanelUIName()
    {
        return panelUIName;
    }

    public void ShowPanel()
    {
        if (gfxPanel == null)
            return;

        if (onPanelOpenedCallback != null)
            onPanelOpenedCallback.Invoke();

        isVisible = true;
        gfxPanel.SetActive(true);
    }

    public void HidePanel()
    {
        if (gfxPanel == null || isPermanent)
            return;

        ClosePanel();
    }

    public void TogglePanel()
    {
        if (isVisible)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    public void ForceHidePanel()
    {
        if (gfxPanel == null)
            return;

        ClosePanel();
    }

    private void ClosePanel()
    {
        isVisible = false;
        gfxPanel.SetActive(false);
    }
}

public enum PanelUIName { HealthBar, Armorbar, EnergyBar, Pause, GameEnd, Minimap, Dialog, LevelChanger }