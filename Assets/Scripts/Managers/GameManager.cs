using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] objectsToKeep;
    private GameObject[] sceneExits;
    private EntryName playerSceneEntryPoint = EntryName.None;
    private PlayerController playerController;

    public bool gameEnded = false;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (objectsToKeep.Length > 0)
        {
            foreach (GameObject obj in objectsToKeep)
            {
                DontDestroyOnLoad(obj);
            }
        }

        playerController = PlayerController.Instance;
        //playerController.SetPlayerInteractable(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleGamePause();
        }
    }

    public void SceneChange(string scene, EntryName entryPoint = EntryName.None)
    {
        //levelChanger.FadeOut();
        //SavePlayerData();
        player.SetActive(false);
        playerSceneEntryPoint = entryPoint;
        SceneManager.sceneLoaded += OnLevelLoaded;
        float animationDuration = 1f;
        StartCoroutine(SceneChangeAfterDelay(scene, animationDuration));
    }

    IEnumerator SceneChangeAfterDelay(string scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //levelChanger.FadeIn();
        if (playerSceneEntryPoint == EntryName.None)
            return;

        Vector2 playerPosition = Vector2.zero;
        sceneExits = GameObject.FindGameObjectsWithTag("SceneEntry");
        if (sceneExits != null && sceneExits.Length > 0)
        {
            foreach (GameObject exit in sceneExits)
            {
                if (exit.GetComponent<SceneEntry>().entryName == playerSceneEntryPoint)
                    playerPosition = exit.GetComponent<SceneEntry>().safeSide.transform.position;
            }
        }
        player.transform.position = playerPosition;
        player.SetActive(true);
        //if (playerController.playerStats.health.CurrentValue <= 0)
        //playerController.RespawnPlayer(); // If health is 0 it's because it died and needs to reset values
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    public void PauseGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
    }

    public void ToggleGamePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            HUDManager.Instance.pauseMenu.ShowPanel();
            Time.timeScale = 0f;
        }
        else
        {
            HUDManager.Instance.pauseMenu.HidePanel();
            Time.timeScale = 1f;
        }

    }
}
