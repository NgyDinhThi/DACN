using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Config")]
    public int numberToSpawn;
    public float spawnDelay;

    [Header("Timing Settings")]
    public float respawnCheckInterval;
    public float deadEnemyClearDelay;
    public float deathAnimationDuration;

    private Collider2D spawnArea;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDied += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDied -= HandleEnemyDeath;
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

            activeEnemies.RemoveAll(e => e == null || !e.activeInHierarchy);

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
        GameObject enemy = EnemyPool.instance.GetEnemy(spawnPos);
        activeEnemies.Add(enemy);
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        StartCoroutine(DelayReturnToPool(enemy));
    }

    private IEnumerator DelayReturnToPool(GameObject enemy)
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        activeEnemies.Remove(enemy);
        EnemyPool.instance.ReturnEnemy(enemy);
    }

    private Vector2 GetRandomPositionInZone()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
