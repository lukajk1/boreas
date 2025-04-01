using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXObject;
    public static SFXManager I;

    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }


    public void PlaySFXClip(AudioClip clip, Vector3 positionToPlaySound, bool varyPitch = true)
    {
        AudioSource audioSource = Instantiate(soundFXObject, positionToPlaySound, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.Play();
        
        if (varyPitch) audioSource.pitch = Random.Range(0.9f, 1.1f);

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }


}
