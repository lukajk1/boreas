using UnityEngine;

public class EnvSFXList : MonoBehaviour
{
    public static EnvSFXList I;
    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }
    [SerializeField] public AudioClip crystalSpawn;
}
