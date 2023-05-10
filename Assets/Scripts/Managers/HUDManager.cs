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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
