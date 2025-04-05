using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Canvas settingsPage;
    [SerializeField] private Button confirm;

    [SerializeField] private Slider sliderFOV;
    [SerializeField] private Slider sliderVolMaster;
    [SerializeField] private Slider sliderVolSFX;
    [SerializeField] private Slider sliderVolMusic;
    [SerializeField] private Slider sliderSensitivity;

    [SerializeField] private TMP_InputField fieldFOV;
    [SerializeField] private TMP_InputField fieldVolMaster;
    [SerializeField] private TMP_InputField fieldVolSFX;
    [SerializeField] private TMP_InputField fieldVolMusic;
    [SerializeField] private TMP_InputField fieldSensitivity;

    [SerializeField] private Toggle toggleVSync;

    public static SettingsManager i;

    private int defaultFOV = 95;
    private int defaultVolMaster = 100;
    private int defaultVolSFX = 100;
    private int defaultVolMusic = 100;
    private float defaultSensitivity = 45f;
    private int defaultVSync = 1;

    public SettingsStruct currentSettings;

    private bool suppressCallback = false; 
    public enum InputType
    {
        Slider,
        Field
    }
    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentSettings = LoadFromPlayerPrefs();
    }

    private void Start()
    {
        settingsPage.gameObject.SetActive(false);

        confirm.onClick.AddListener(SaveAndClose);

        sliderFOV.onValueChanged.AddListener(value => SetAndSyncValue(InputType.Slider, value, sliderFOV, fieldFOV));
        fieldFOV.onEndEdit.AddListener(_ => SetAndSyncValue(InputType.Field, 0f, sliderFOV, fieldFOV)); // ignore 'value' for field

        sliderVolMaster.onValueChanged.AddListener(value => SetAndSyncValue(InputType.Slider, value, sliderVolMaster, fieldVolMaster));
        fieldVolMaster.onEndEdit.AddListener(_ => SetAndSyncValue(InputType.Field, 0f, sliderVolMaster, fieldVolMaster));

        sliderVolSFX.onValueChanged.AddListener(value => SetAndSyncValue(InputType.Slider, value, sliderVolSFX, fieldVolSFX));
        fieldVolSFX.onEndEdit.AddListener(_ => SetAndSyncValue(InputType.Field, 0f, sliderVolSFX, fieldVolSFX));

        sliderVolMusic.onValueChanged.AddListener(value => SetAndSyncValue(InputType.Slider, value, sliderVolMusic, fieldVolMusic));
        fieldVolMusic.onEndEdit.AddListener(_ => SetAndSyncValue(InputType.Field, 0f, sliderVolMusic, fieldVolMusic)); 

        sliderSensitivity.onValueChanged.AddListener(value => SetAndSyncValue(InputType.Slider, value, sliderSensitivity, fieldSensitivity));
        fieldSensitivity.onEndEdit.AddListener(_ => SetAndSyncValue(InputType.Field, 0f, sliderSensitivity, fieldSensitivity));

        toggleVSync.onValueChanged.AddListener(isOn => currentSettings.VSync = isOn ? 1 : 0);

        SetUIFromStruct(currentSettings);
        ApplySettingsToGame(currentSettings);
    }


    private void SetAndSyncValue(InputType inputType, float value, Slider slider, TMP_InputField field)
    {
        if (suppressCallback) return;
        suppressCallback = true;

        if (inputType == InputType.Slider)
        {
            field.text = slider.value.ToString();
        }
        else
        {
            if (float.TryParse(field.text, out float fieldValue))
            {
                slider.value = Mathf.Clamp(fieldValue, slider.minValue, slider.maxValue);
            }

        }
        SetToStructFromUI();
        suppressCallback = false;
    }

    // whatever it's so negligible in terms of performance I'll just do it this way. If I need something more extensible then I can do that later
    private void SetToStructFromUI()
    {
        currentSettings.FOV = (int)sliderFOV.value;
        currentSettings.VolMaster = (int)sliderVolMaster.value;
        currentSettings.VolSFX = (int)sliderVolSFX.value;
        currentSettings.VolMusic = (int)sliderVolMusic.value;
        currentSettings.MouseSensitivity = sliderSensitivity.value;
    }
    private void SetUIFromStruct(SettingsStruct settings)
    {
        sliderFOV.value = settings.FOV;
        sliderVolMaster.value = settings.VolMaster;
        sliderVolSFX.value = settings.VolSFX;
        sliderVolMusic.value = settings.VolMusic;
        sliderSensitivity.value = settings.MouseSensitivity;
    }

    private SettingsStruct LoadFromPlayerPrefs()
    {
        int fov, volMaster, volSFX, volMusic, vsync;
        float mouseSensitivity;

        if (PlayerPrefs.HasKey("FOV")) { fov = PlayerPrefs.GetInt("FOV"); }
        else { fov = defaultFOV; }

        if (PlayerPrefs.HasKey("VolMaster")) { volMaster = PlayerPrefs.GetInt("VolMaster"); }
        else {  volMaster = defaultVolMaster; }

        if (PlayerPrefs.HasKey("VolSFX")) { volSFX = PlayerPrefs.GetInt("VolSFX"); }
        else { volSFX = defaultVolSFX; }

        if (PlayerPrefs.HasKey("VolMusic")) { volMusic = PlayerPrefs.GetInt("VolMusic"); }
        else { volMusic = defaultVolMusic; }

        if (PlayerPrefs.HasKey("MouseSensitivity")) { mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity"); }
        else { mouseSensitivity = defaultSensitivity; }

        if (PlayerPrefs.HasKey("VSync")) { vsync = PlayerPrefs.GetInt("VSync"); }
        else { vsync = defaultVSync; }

        return new SettingsStruct(fov, volMaster, volSFX, volMusic, mouseSensitivity, vsync);
    }

    private void ApplySettingsToGame(SettingsStruct settings)
    {
        SoundMixerManager manager = FindAnyObjectByType<SoundMixerManager>();
        manager.SetMasterVolume(settings.VolMaster / 100f);
        manager.SetSFXVolume(settings.VolSFX / 100f);
        manager.SetMusicVolume(settings.VolMusic / 100f);

        CameraDampen dampen = FindAnyObjectByType<CameraDampen>();
        if (dampen != null) 
        { 
            dampen.GetComponent<Camera>().fieldOfView = settings.FOV;
        }

        QualitySettings.vSyncCount = settings.VSync; // 0 = off, 1 = match refresh rate

    }

    public void Open() 
    {
        settingsPage.gameObject.SetActive(true);
    }

    void SaveAndClose()
    {
        currentSettings.Save();
        ApplySettingsToGame(currentSettings);
        settingsPage.gameObject.SetActive(false);
    }

}
