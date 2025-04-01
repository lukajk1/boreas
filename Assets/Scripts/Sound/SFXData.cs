using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SFXData
{
    public List<(AudioClip, SFX)> soundFXList;
    public enum GunSFX
    {
        GenericReload,
        GenericOutOfAmmo
    }
    public enum SFX
    {
        GenericReload,
        GenericOutOfAmmo
    }
}
