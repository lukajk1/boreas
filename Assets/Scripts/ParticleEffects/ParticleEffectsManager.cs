using UnityEngine;
using System.Collections.Generic;

public class ParticleEffectsManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodSplatter;
    private Queue<ParticleSystem> pool = new Queue<ParticleSystem>();

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            var instance = Instantiate(bloodSplatter, transform);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    private void OnEnable()
    {
        WeaponEventBus.OnEnemyHit += BloodSplatter;
    }

    private void OnDisable()
    {
        WeaponEventBus.OnEnemyHit -= BloodSplatter;
    }

    private void BloodSplatter(Vector3 pos)
    {
        if (pool.Count == 0) return;

        var ps = pool.Dequeue();
        ps.transform.position = pos;
        ps.gameObject.SetActive(true);
        ps.Play();

        StartCoroutine(ReturnToPoolAfterPlay(ps));
    }

    private System.Collections.IEnumerator ReturnToPoolAfterPlay(ParticleSystem ps)
    {
        yield return new WaitForSeconds(ps.main.duration);
        ps.gameObject.SetActive(false);
        pool.Enqueue(ps);
    }
}
