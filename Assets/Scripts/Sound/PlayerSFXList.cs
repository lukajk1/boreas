using UnityEngine;

public class PlayerSFXList : MonoBehaviour
{
    public static PlayerSFXList I;
    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }
    [SerializeField] public AudioClip walljump;
    [SerializeField] public AudioClip slowfall;
    [SerializeField] public AudioClip jumpTakeoff;
    [SerializeField] public AudioClip jumpLanding;
    [SerializeField] public AudioClip slide;
    [SerializeField] public AudioClip hurt;
}
