using System;
using UnityEngine;

/// <summary>
/// Quản lý máu, sát thương và cái chết của enemy.
/// </summary>
public class EnemyHealth : MonoBehaviour, IdamageAble
{
    public static event Action OnEnemyDeathEvent;

    [Header("Thiết lập máu")]
    [SerializeField] private float maxHealth ;

    public float CurrentHealth { get; private set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelect enemySelect;
    private EnemyLoot enemyLoot;
    private Rigidbody2D rb2d;

    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelect = GetComponent<EnemySelect>();
        enemyLoot = GetComponent<EnemyLoot>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Gọi khi enemy nhận sát thương.
    /// </summary>
    /// <param name="amount">Lượng sát thương nhận</param>
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        if (CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            DmgManager.instance?.hienSatthuong(amount, transform);
        }
    }

    /// <summary>
    /// Xử lý khi enemy chết.
    /// </summary>
    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Death");
        enemyBrain.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;

        enemySelect?.NoSelectedCallback();
        OnEnemyDeathEvent?.Invoke();

        // Thêm kinh nghiệm cho người chơi
        GameManager.instance?.AddPlayerExp(enemyLoot.ExpDrop);

        // Để animation có thời gian chạy trước khi destroy (tuỳ ý)
        StartCoroutine(DelayedDisable());
    }

    private System.Collections.IEnumerator DelayedDisable()
    {
        yield return new WaitForSeconds(2f); // thời gian animation chết

        gameObject.SetActive(false); // hoặc Destroy(gameObject)
    }
}
