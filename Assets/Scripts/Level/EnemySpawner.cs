using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    public static EnemySpawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Vector3> spawnPoints;
    public List<GameObject> enemyTypes;
    public List<EnemyWave> waves;
    public float enemyScale = 1f;
    private int waveIdx = 0;
    private EnemyWave currentWave;
    [SerializeField] private AnimationCurve difficulty;
    [SerializeField] int baseEnemyAmmount = 5;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (EnemyWave wave in waves)
        {
            wave.enemyAmmount = Mathf.FloorToInt(difficulty.Evaluate(i) * baseEnemyAmmount);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave != null)
        {
            if (currentWave.state == WaveState.FINISHED)
            {
                if (waveIdx < waves.Count)
                {
                    StartCoroutine(SpawnWave(3f));
                }
                else if (!GameManager.Instance.gameEnded)
                {
                    Debug.Log("Victory!");
                    GameManager.Instance.gameEnded = true;
                    SceneExit.Instance.Activate();
                    //HUDManager.Instance.gameEnd.Victory();
                }
            }
        }
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnWave(.1f));
    }

    private IEnumerator SpawnWave(float delay)
    {
        currentWave = waves[waveIdx];
        currentWave.state = WaveState.SPAWNING;
        int randomPos;
        int randomEnemy;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < currentWave.enemyAmmount; i++)
        {
            randomPos = Mathf.FloorToInt(Random.Range(0f, spawnPoints.Count));
            randomEnemy = Mathf.FloorToInt(Random.Range(0f, enemyTypes.Count));
            GameObject enemy = Instantiate<GameObject>(enemyTypes[randomEnemy], spawnPoints[randomPos], Quaternion.identity);
            enemy.transform.parent = MapCreator.Instance.interactable;
            enemy.transform.localScale = Vector3.one * enemyScale;
            currentWave.activeEnemies.Add(enemy);
        }
        currentWave.state = WaveState.WAITING;
        waveIdx++;
    }

    public void KillEnemy(GameObject enemy)
    {
        if (currentWave == null)
            return;

        currentWave.activeEnemies.Remove(enemy);
        if (currentWave.activeEnemies.Count <= 0)
            currentWave.state = WaveState.FINISHED;
    }
}

public enum WaveState { SPAWNING, WAITING, FINISHED }

[System.Serializable]
public class EnemyWave
{
    public int enemyAmmount;
    public WaveState state;
    public List<GameObject> activeEnemies;
}