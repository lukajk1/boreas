using System.Collections;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    private float waitPeriod;
    private GameObject enemyPrefab;
    private Vector3 spawnPos;

    public void Setup(float waitPeriod, GameObject enemyPrefab)
    {
        this.waitPeriod = waitPeriod;
        this.enemyPrefab = enemyPrefab;
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        SFXManager.I.PlaySFXClip(SFXManager.SoundType._3D, EnemySFXList.I.enemySpawn, transform.position);
        yield return new WaitForSeconds(waitPeriod);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.05f); // juust to make sure the instantiation goes through. idk if this actually does anything or not
        Destroy(gameObject);
    }
}
