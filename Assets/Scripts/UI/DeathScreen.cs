using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;


public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private ChromaticAberration chromaticAberration;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private TextMeshProUGUI runTime;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI shotsFired;
    [SerializeField] private TextMeshProUGUI damageDealt;
    [SerializeField] private TextMeshProUGUI damageTaken;
    [SerializeField] private TextMeshProUGUI criticalHits;
    [SerializeField] private TextMeshProUGUI totalHitsLanded;

    [SerializeField] private Button goAgain;
    [SerializeField] private Button quit;

    private float windDownTimeLength = 2.5f;
    private PlayerInput playerInput;

    private void OnEnable()
    {
        MainEventBus.OnRunEnd += OnRunEnd;
    }
    private void OnDisable()
    {
        MainEventBus.OnRunEnd -= OnRunEnd;
    }
    private void Start()
    {
        volume.profile.TryGet(out chromaticAberration);
        deathScreen.SetActive(false);

        goAgain.onClick.AddListener(() => ReloadScene());
        quit.onClick.AddListener(() => Application.Quit());
        
        playerInput = FindFirstObjectByType<PlayerInput>();

    }
    private void Update()
    {
    }
    private void OnRunEnd()
    {
        RunStats stats = FindFirstObjectByType<RunStatsManager>().RequestStats();

        int minutes = (int)(stats.elapsedRunTime / 60f);
        int seconds = (int)(stats.elapsedRunTime % 60f);

        runTime.text = $"run length: {minutes:00}:{seconds:00}";
        score.text = $"score: {stats.score}";
        //shotsFired.text = $"shots fired: {stats.shotsFired:N0}";
        enemiesKilled.text = $"enemies slain: {stats.enemiesKilled:N0}";
        damageDealt.text = $"damage dealt: {stats.damageDealt:N0}";
        damageTaken.text = $"damage taken: {stats.damageTaken:N0}";
        criticalHits.text = $"critical hit %: {(stats.criticalHits * 100f / stats.totalHitsLanded):0.0}%";
        totalHitsLanded.text = $"overall accuracy: {(stats.totalHitsLanded * 100f / stats.shotsFired):0.0}%";

        deathScreen.SetActive(true);
        //Game.MenusOpen++;
        
        StartCoroutine(WindDownTime());
        chromaticAberration.intensity.value = 0.5f;

        AudioListener.pause = true;
        playerInput.actions.FindActionMap("Player").Disable(); // disable player action map to prevent new inputs

        Game.CursorLocked = false;
    } 

    private IEnumerator WindDownTime()
    {
        float elapsed = 0f;
        while (elapsed <= windDownTimeLength)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / windDownTimeLength);
            Time.timeScale = Mathf.Lerp(0.15f, 0.03f, t);

            yield return null;
        }
        Time.timeScale = 0.03f;

    }

    private void ReloadScene()
    {
        Debug.Log("reload scene clicked");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}