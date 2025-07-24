using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IdamageAble
{
    public static event Action<GameObject> OnEnemyDied;
    public static event Action OnEnemyDeathEvent;

    [Header("Thiết lập máu")]
    [SerializeField] private float maxHealth;

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

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void ResetEnemy()
    {
        isDead = false;
        CurrentHealth = maxHealth;

        enemyBrain.enabled = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

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
        AudioManager.instance.Play("HitEnemy");

    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Death");
        enemyBrain.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;

        enemySelect?.NoSelectedCallback();
        OnEnemyDied?.Invoke(gameObject);
        OnEnemyDeathEvent?.Invoke();

        GameManager.instance?.AddPlayerExp(enemyLoot.ExpDrop);
        QuestManager.instance.OnEnemyKilled(gameObject);
    }
}
