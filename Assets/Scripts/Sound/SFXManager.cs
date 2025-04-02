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

    public enum SoundType
    {
        _3D
    }

    public void PlaySFXClip(AudioClip clip, Vector3 positionToPlaySound, bool varyPitch = true)
    {
        AudioSource audioSource = Instantiate(soundFXObject, positionToPlaySound, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.Play();
        
        if (varyPitch) audioSource.pitch = Random.Range(0.9f, 1.1f);

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }    
    
    public void PlaySFXClip(SoundType type, AudioClip clip, Vector3 positionToPlaySound, bool varyPitch = true)
    {
        AudioSource audioSource = Instantiate(soundFXObject, positionToPlaySound, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.Play();
        audioSource.spatialBlend = 1;
        audioSource.minDistance = 20f;
        audioSource.maxDistance = 20f;
        
        if (varyPitch) audioSource.pitch = Random.Range(0.9f, 1.1f);

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }


}
