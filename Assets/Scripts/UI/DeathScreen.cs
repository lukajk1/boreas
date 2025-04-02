using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System.Runtime.CompilerServices;


public class DeathScreen : MonoBehaviour
{
    [SerializeField] private PostProcessVolume volume;
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
        volume.profile.TryGetSettings(out chromaticAberration);
        deathScreen.SetActive(false);

        goAgain.onClick.AddListener(() => ReloadScene());
        quit.onClick.AddListener(() => Application.Quit());
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
        chromaticAberration.intensity.value = 0.42f;

        FindFirstObjectByType<AudioListener>().enabled = false;
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}