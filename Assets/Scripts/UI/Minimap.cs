using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : PanelUI
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TogglePanel();
        }
    }
}
