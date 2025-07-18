using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Config")]
    public GameObject[] enemyPrefabs;
    public int numberToSpawn;
    public float spawnDelay;

    [Header("Timing Settings")]
    public float respawnCheckInterval; // thời gian kiểm tra để respawn quái
    public float deadEnemyClearDelay;  // thời gian delay để xóa quái đã chết

    private Collider2D spawnArea;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(InitialSpawn());
        StartCoroutine(CheckAndRespawn());
    }

    private IEnumerator InitialSpawn()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnCheckInterval);

            // Đợi thời gian delay trước khi dọn dẹp quái chết
            yield return new WaitForSeconds(deadEnemyClearDelay);

            // Xóa enemy null hoặc bị disable (chết)
            activeEnemies.RemoveAll(e => e == null || !e.activeInHierarchy);

            // Spawn thêm nếu thiếu
            int missing = numberToSpawn - activeEnemies.Count;
            for (int i = 0; i < missing; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }

            Debug.Log($"[SPAWNER] Enemy count: {activeEnemies.Count}/{numberToSpawn}");
        }
    }

    private void SpawnEnemy()
    {
        if (activeEnemies.Count >= numberToSpawn) return;

        Vector2 spawnPos = GetRandomPositionInZone();
        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(selectedEnemy, spawnPos, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    private Vector2 GetRandomPositionInZone()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
