using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
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
        shotsFired.text = $"shots fired: {stats.shotsFired:N0}";
        enemiesKilled.text = $"enemies slain: {stats.enemiesKilled:N0}";
        damageDealt.text = $"total damage dealt: {stats.damageDealt:N0}";
        damageTaken.text = $"total damage taken: {stats.damageTaken:N0}";
        criticalHits.text = $"critical hit %: {(stats.criticalHits * 100f / stats.totalHitsLanded):0.0}%";
        totalHitsLanded.text = $"overall accuracy: {(stats.totalHitsLanded * 100f / stats.shotsFired):0.0}%";

        deathScreen.SetActive(true);
        Game.MenusOpen++;
    } 

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}