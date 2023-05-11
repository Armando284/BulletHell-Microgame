using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    #region Singleton 

    public static SceneExit Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private GameObject gfx;
    private bool isActive = false;

    private void Start()
    {
        DeActivate();
    }

    public void Activate()
    {
        isActive = true;
        gfx.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DeActivate()
    {
        isActive = false;
        gfx.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.IsLastScene())
            {
                HUDManager.Instance.gameEnd.Victory();
            }
            else
            {
                GameManager.Instance.GoNextScene();
            }
        }
    }
}
