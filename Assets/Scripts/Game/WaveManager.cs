using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager I;
    [SerializeField] private GameObject crystal;
    [Header("enemy prefabs")]
    [SerializeField] private GameObject zomboPrefab;
    [SerializeField] private GameObject casterPrefab;
    [SerializeField] private GameObject diverPrefab;
    [SerializeField] private GameObject bouncyPrefab;

    private List<WaveData> arena1WavePool;
    private List<WaveData> arena2WavePool;

    private Vector3 a1CrystalSpawnPos = new Vector3(39.5200005f, 51.2200012f, -26.7399998f);

    [SerializeField] private GameObject arena1SpawnpointsParent;
    private List<Vector3> arena1Spawnpoints = new List<Vector3>();

    private int wave = 0;
    private int currentWaveSize = 0;
    private int remainingEnemiesInWave = 0;
    private bool isSpawning = true;

    private void Awake()
    {
        if (I != null) Debug.LogError("multiple wavespawners");
        I = this;

        arena1WavePool = new List<WaveData>
        {
            new WaveData(3, 5, new List<GameObject> { zomboPrefab, casterPrefab } ),
            new WaveData(4, 6, new List<GameObject> { zomboPrefab, casterPrefab } ),
            new WaveData(7, 8, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab } ),
            new WaveData(7, 8, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab, diverPrefab } ),
            new WaveData(8, 9, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab, diverPrefab } )
        };

        foreach (Transform t in arena1SpawnpointsParent.transform)
        {
            arena1Spawnpoints.Add(t.position);
            t.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        MainEventBus.OnRunStart += OnRunStart;
        CombatEventBus.OnEnemyDeath += OnEnemyDeath;
    }
    private void OnDisable()
    {
        MainEventBus.OnRunStart -= OnRunStart;
        CombatEventBus.OnEnemyDeath -= OnEnemyDeath;
    }
    void OnRunStart()
    {
        SpawnNextWave();
    }
    void SpawnNextWave()
    {

        if (wave < arena1WavePool.Count)
        {
            wave++;
            WaveData nextWave = arena1WavePool[wave - 1]; 
            
            currentWaveSize = Random.Range(nextWave.minEnemies, nextWave.maxEnemies + 1);
            remainingEnemiesInWave = currentWaveSize;

            for (int i = 0; i < currentWaveSize; i++)
            {
                // instantiate random enemytype at random spawnpoint
                Instantiate(
                    nextWave.enemyTypes[Random.Range(0, nextWave.enemyTypes.Count)],
                    arena1Spawnpoints[Random.Range(0, arena1Spawnpoints.Count)] + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized, // add random xz vector of mag 1 to prevent enemies from spawning inside each other, dunno if it matters but no reason to invite extra jank 
                    Quaternion.identity);
            }
        }
        else
        {
            Instantiate(crystal, a1CrystalSpawnPos, Quaternion.identity);
            isSpawning = false;
        }



    }

    private void Update()
    {
        if (remainingEnemiesInWave == 0 && isSpawning) SpawnNextWave();    
    }

    void OnEnemyDeath()
    {
        remainingEnemiesInWave--;
    }

    public bool CommandSpawnEnemy(string name)
    {
        return true;
    }
}
