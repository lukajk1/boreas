using UnityEngine;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner I;
    [SerializeField] private GameObject groundEnemy;

    private Vector3 spawnPos = new Vector3(73.5670013f, 57.9500008f, -33.6399994f);

    private List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        if (I != null)
        {
            Debug.LogError("multiple wavespawners");
        }
        I = this;
    }

    public bool SpawnEnemy(string name)
    {
        if (name == "groundenemy")
        {
            Instantiate(groundEnemy, spawnPos, Quaternion.identity);
            return true;
        }
        else return false;
    }
}
