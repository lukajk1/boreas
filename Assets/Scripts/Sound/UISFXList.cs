using UnityEngine;

public class UISFXList : MonoBehaviour
{
    public static UISFXList I;
    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }
    [SerializeField] public AudioClip reloadFinished;
    [SerializeField] public AudioClip outOfBullets;
    [SerializeField] public AudioClip enemyCritHit;
    [SerializeField] public AudioClip enemyBodyHit;
    [SerializeField] public AudioClip weaponFire;
    [SerializeField] public AudioClip enemyDeath;
}
