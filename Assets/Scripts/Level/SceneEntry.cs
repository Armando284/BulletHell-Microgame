using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Play enter effect
        }
    }
}

public enum EntryName { None, RespawnStatue, TownRight, Level1Left }