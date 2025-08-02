using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TMP_Text waveCountdownText;

    [Header("Wave Info")]
    [SerializeField] private int currentWave = 0;
    [SerializeField] private int enemiesAlive = 0;

    private List<GameObject> currentEnemies = new List<GameObject>();
    private bool autoWaveEnabled = true;
    private bool forceNextWave = false;
    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaveLoop());
    }

    private System.Collections.IEnumerator SpawnWaveLoop()
    {
        while (true)
        {
            // Wait here if autoWave is off and no manual trigger
            while (!autoWaveEnabled && !forceNextWave)
            {
                waveCountdownText.text = "Waves Paused...";
                yield return null;
            }

            waveCountdownText.text = "";

            currentWave++;
            int enemyCount = GetEnemyCountForWave(currentWave);
            Debug.Log($"Spawning Wave {currentWave} with {enemyCount} enemies");

            currentEnemies = enemySpawner.SpawnEnemies(enemyCount);
            enemiesAlive = currentEnemies.Count;
            isSpawning = true;

            // Wait until all enemies are dead or next wave is forced
            while (enemiesAlive > 0 && !forceNextWave)
            {
                currentEnemies.RemoveAll(e => e == null);
                enemiesAlive = currentEnemies.Count;
                yield return new WaitForSeconds(1f);
            }

            forceNextWave = false;

            // Wait countdown before next wave (if auto is on)
            if (autoWaveEnabled)
            {
                float countdown = 5f;
                while (countdown > 0f && !forceNextWave)
                {
                    waveCountdownText.text = $"Next wave in: {Mathf.Ceil(countdown)}";
                    countdown -= Time.deltaTime;
                    yield return null;
                }

                waveCountdownText.text = "";
            }

            isSpawning = false;
        }
    }

    private int GetEnemyCountForWave(int wave)
    {
        switch (wave)
        {
            case 1: return 30;
            case 2: return 50;
            case 3: return 70;
            default: return 70 + ((wave - 3) * 10);
        }
    }

    // --- Public API for GameManager ---

    public int CurrentWave => currentWave;
    public int EnemiesAlive => enemiesAlive;

    public void SetAutoWave(bool isOn)
    {
        autoWaveEnabled = isOn;
    }

    public void SpawnNextWaveNow()
    {
        if (!isSpawning)
            autoWaveEnabled = true; // ensures the loop resumes

        forceNextWave = true;
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in currentEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        currentEnemies.Clear();
        enemiesAlive = 0;
    }
}