using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager I;
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject spawnIndicator;
    [Header("enemy prefabs")]
    [SerializeField] private GameObject zomboPrefab;
    [SerializeField] private GameObject casterPrefab;
    [SerializeField] private GameObject diverPrefab;
    [SerializeField] private GameObject bouncyPrefab;

    private List<WaveData> arena1WavePool;
    private List<WaveData> arena2WavePool;

    private Vector3 a1CrystalSpawnPos = new Vector3(21.2000008f, 51.2200012f, -8.5f);

    [SerializeField] private GameObject arena1SpawnpointsParent;
    private List<Vector3> arena1Spawnpoints = new List<Vector3>();

    private int wave = 0;
    private int currentWaveSize = 0;
    private int remainingEnemiesInWave = 0;
    private bool isSpawning = true;

    private int lastWaveSize = 7;
    private float minRangeFromPlayerForSpawning = 12f;
    private float spawnDelay = 1.5f;
    private bool initialSpawn;

    private void Awake()
    {
        if (I != null) Debug.LogError("multiple wavespawners");
        I = this;

        arena1WavePool = new List<WaveData>
        {
            new WaveData(5, 8, new List<GameObject> { zomboPrefab, casterPrefab } ),
            new WaveData(5, 8, new List<GameObject> { zomboPrefab, casterPrefab } ),
            new WaveData(9, 11, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab } ),
            new WaveData(10, 13, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab, diverPrefab } ),
            new WaveData(10, 13, new List<GameObject> { zomboPrefab, casterPrefab, bouncyPrefab, diverPrefab } )
        };
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

    private void Start()
    {
        RepositionSpawnpoint[] points = GetComponentsInChildren<RepositionSpawnpoint>();
        foreach (var point in points)
        {
            point.Reposition();
        }

        StartCoroutine(WaitOneFrame());
    }

    private void Update()
    {
        if (remainingEnemiesInWave <= lastWaveSize / 2 && isSpawning && !initialSpawn)
        {
            SpawnNextWave();
        }
        if (FindAnyObjectByType<DummySystem>().SpawningEnabled && initialSpawn)
        {
            initialSpawn = false;
            SpawnNextWave();
        }
    }

    private IEnumerator WaitOneFrame()
    {
        yield return null;
        foreach (Transform t in arena1SpawnpointsParent.transform)
        {
            arena1Spawnpoints.Add(t.position);
            t.gameObject.SetActive(false);
        }
        initialSpawn = true;
        Debug.Log(arena1Spawnpoints.Count);
    }

    void OnRunStart()
    {

    }
    void SpawnNextWave()
    {

        if (wave < arena1WavePool.Count)
        {
            wave++;
            WaveData nextWave = arena1WavePool[wave - 1];

            lastWaveSize = currentWaveSize; // set last wave size before updating currentwave to the new wave size 
            currentWaveSize = Random.Range(nextWave.minEnemies, nextWave.maxEnemies + 1);

            remainingEnemiesInWave = currentWaveSize;

            for (int i = 0; i < currentWaveSize; i++)
            {
                Vector3 prospectiveSpawnpoint;
                int attempts = 0;
                do
                {
                    if (arena1Spawnpoints.Count > 1)
                    {
                        prospectiveSpawnpoint = arena1Spawnpoints[Random.Range(0, arena1Spawnpoints.Count)];
                    }
                    else
                    {
                        Debug.Log(arena1Spawnpoints.Count);
                        prospectiveSpawnpoint = arena1Spawnpoints[0];
                    }
                    if (attempts > 100) break; // better to have some safety..
                }
                while (Vector3.Distance(prospectiveSpawnpoint, Game.i.PlayerTransform.position) < minRangeFromPlayerForSpawning);
                

                //// instantiate random enemytype at random spawnpoint
                //Instantiate(
                //    nextWave.enemyTypes[Random.Range(0, nextWave.enemyTypes.Count)],
                //    arena1Spawnpoints[Random.Range(0, arena1Spawnpoints.Count)] + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized, // add random xz vector of mag 1 to prevent enemies from spawning inside each other, dunno if it matters but no reason to invite extra jank 
                //    Quaternion.identity);

                GameObject spawnIndicatorInstance = Instantiate(spawnIndicator, prospectiveSpawnpoint + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized, Quaternion.identity);

                GameObject prefab = nextWave.enemyTypes[Random.Range(0, nextWave.enemyTypes.Count)];
                //Debug.Log(prefab);
                spawnIndicatorInstance.GetComponent<SpawnIndicator>().Setup(spawnDelay, prefab);

            }
        }
        else
        {
            Instantiate(crystal, a1CrystalSpawnPos, Quaternion.identity);
            isSpawning = false;
        }



    }


    void OnEnemyDeath(EnemyUnit enemyUnit, Vector3 pos)
    {
        remainingEnemiesInWave--;
    }

    public bool CommandSpawnEnemy(string name)
    {
        return true;
    }
}
