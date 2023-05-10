using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Common
{
    /// <summary>
    /// Returns true if mouse is over an UI element whith Raycast Target
    /// </summary>
    /// <returns>bool</returns>
    public static bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();
}
