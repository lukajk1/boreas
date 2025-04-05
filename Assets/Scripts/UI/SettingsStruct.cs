using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct SettingsStruct
{
    public int FOV;
    public int VolMaster;
    public int VolSFX;
    public int VolMusic;
    public float MouseSensitivity;
    public int VSync;

    public SettingsStruct(int FOV, int VolMaster, int VolSFX, int VolMusic, float MouseSensitivity, int VSync)
    {
        this.FOV = Mathf.Clamp(FOV, 10, 110);
        this.VolMaster = Mathf.Clamp(VolMaster, 0, 100);
        this.VolSFX = Mathf.Clamp(VolSFX, 0, 100);
        this.VolMusic = Mathf.Clamp(VolMusic, 0, 100);   
        this.MouseSensitivity = MouseSensitivity;
        this.VSync = Mathf.Clamp(VSync, 0, 1);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("FOV", FOV);
        PlayerPrefs.SetInt("VolMaster", VolMaster);
        PlayerPrefs.SetInt("VolSFX", VolSFX);
        PlayerPrefs.SetInt("VolMusic", VolMusic);
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
        PlayerPrefs.SetFloat("VSync", VSync);
    }
}
