using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region Singleton

    public static HUDManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameEnd gameEnd;
    public HealthBar healthBar;
    public EnergyBar energyBar;
    public PauseMenu pauseMenu;
    public Minimap minimap;

    private PanelUI[] panels;

    private void Start()
    {
        panels = GetComponentsInChildren<PanelUI>();
        if (panels.Length <= 0)
            return;

        foreach (PanelUI panel in panels)
        {
            panel.onPanelOpenedCallback += CleanOpenPanels;
        }
    }

    public void CleanOpenPanels()
    {
        foreach (PanelUI panel in panels)
        {
            panel.HidePanel();
        }
    }

}
