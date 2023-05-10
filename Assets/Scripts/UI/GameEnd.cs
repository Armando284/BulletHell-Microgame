using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : PanelUI
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;

    public void GameOver()
    {
        if (gameWonPanel.activeSelf)
            gameWonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        ShowPanel();
    }

    public void Victory()
    {
        if (gameOverPanel.activeSelf)
            gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(true);
        ShowPanel();
    }
}
