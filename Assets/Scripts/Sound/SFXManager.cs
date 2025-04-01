using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager I;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (I != null) Debug.LogError("too many instances");
        I = this;
    }


    public void PlaySFXClip(AudioClip clip, Transform positionToPlaySound, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, positionToPlaySound.position, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
