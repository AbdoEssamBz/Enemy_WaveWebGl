using TMPro;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesAliveText;
    [SerializeField] private TMP_Text fpsText;

    [Header("Wave Control")]
    [SerializeField] private WaveSystem waveSystem;

    private bool autoWaveCycle = true;
    private float deltaTime;

    void Update()
    {
        UpdateUI();
        UpdateFPS();
    }

    void UpdateUI()
    {
        waveText.text = $"Wave: {waveSystem.CurrentWave}";
        enemiesAliveText.text = $"Enemies Alive: {waveSystem.EnemiesAlive}";
    }

    void UpdateFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }

    public void ToggleAutoWaveCycle()
    {
        autoWaveCycle = !autoWaveCycle;
        waveSystem.SetAutoWave(autoWaveCycle);

        // Freeze/unfreeze time
        Time.timeScale = autoWaveCycle ? 1f : 0f;
    }

    public void ForceNextWave()
    {
        waveSystem.SpawnNextWaveNow();
    }

    public void DestroyCurrentWave()
    {
        waveSystem.DestroyAllEnemies();
    }

    public bool IsAutoWaveOn() => autoWaveCycle;
}


