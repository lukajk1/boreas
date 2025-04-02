using UnityEngine;

public class EnemySFXList : MonoBehaviour
{
    public static EnemySFXList I;
    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }
    [SerializeField] public AudioClip enemySpawn;
    [SerializeField] public AudioClip casterAttack;
}
