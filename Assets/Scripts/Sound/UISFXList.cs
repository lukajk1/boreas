using UnityEngine;

public class UISFXList : MonoBehaviour
{
    public static UISFXList i;
    private void Awake()
    {
        if (i != null) Debug.LogError("too many instances");
        i = this;
    }
    [SerializeField] public AudioClip reloadFinished;
    [SerializeField] public AudioClip outOfBullets;
    [SerializeField] public AudioClip enemyCritHit;
    [SerializeField] public AudioClip enemyBodyHit;
    [SerializeField] public AudioClip weaponFire;
    [SerializeField] public AudioClip enemyDeath;
}
