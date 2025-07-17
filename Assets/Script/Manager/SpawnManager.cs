using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Config")]
    public GameObject[] enemyPrefabs;
    public int numberToSpawn ;
    public float spawnDelay = 1f;

    private Collider2D spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector2 spawnPos = GetRandomPositionInZone();
            GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(selectedEnemy, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector2 GetRandomPositionInZone()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
