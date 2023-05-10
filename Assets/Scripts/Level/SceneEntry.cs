using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntry : MonoBehaviour
{
    public EntryName entryName; // This entry's name
    public string targetSceneName; // Name of the scene to navigate to
    public EntryName targetEntrance; // Entry on the target scene to go to
    public bool isExit = true;
    public Transform safeSide; // Place to spawn the player away from the exit collider

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (isExit)
    //        {
    //            GameManager.Instance.SceneChange(targetSceneName, targetEntrance);
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isExit)
            {
                GameManager.Instance.SceneChange(targetSceneName, targetEntrance);
            }
        }
    }
}

public enum EntryName { None, RespawnStatue, TownRight, Level1Left }