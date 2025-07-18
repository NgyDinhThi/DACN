using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý Object Pool cho nhiều loại enemy prefab.
/// </summary>
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [Header("Pool Config")]
    public List<GameObject> enemyPrefabs;
    public int poolSizePerType = 10;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        // Tạo pool cho từng loại enemy
        foreach (GameObject prefab in enemyPrefabs)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < poolSizePerType; i++)
            {
                var obj = Instantiate(prefab);
                obj.SetActive(false);
                obj.transform.position = Vector3.zero;
                queue.Enqueue(obj);
            }
            pools[prefab.name] = queue;
        }
    }

    /// <summary>
    /// Lấy enemy từ pool, đặt tại vị trí chỉ định.
    /// </summary>
    public GameObject GetEnemy(Vector2 position)
    {
        if (enemyPrefabs.Count == 0) return null;

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        string key = prefab.name;

        GameObject enemy = pools.ContainsKey(key) && pools[key].Count > 0
            ? pools[key].Dequeue()
            : Instantiate(prefab);

        // Ép z = 0 để hiển thị đúng trong camera
        enemy.transform.position = new Vector3(position.x, position.y, 0);
        enemy.SetActive(true);
        return enemy;
    }

    /// <summary>
    /// Trả enemy về pool và tắt nó đi.
    /// </summary>
    public void ReturnEnemy(GameObject enemy)
    {
        string key = enemy.name.Replace("(Clone)", "").Trim();
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;

        if (!pools.ContainsKey(key))
            pools[key] = new Queue<GameObject>();

        pools[key].Enqueue(enemy);
    }
}
