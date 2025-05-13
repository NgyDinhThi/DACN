using System;
using UnityEngine;

// Script quản lý máu và sát thương của enemy
public class EnemyHealth : MonoBehaviour, IdamageAble
{
    public static event Action OnEnemyDeathEvent;

    [Header("Config")]
    [SerializeField] private float health; // Máu ban đầu của enemy

    public float mauhientai { get; private set; } // Máu hiện tại (public readonly)

    private Animator animator; // Điều khiển animation chết
    private EnemyBrain enemyBrain; // AI điều khiển hành vi enemy
    private EnemySelect enemySelect; // Hiển thị sprite chọn enemy
    private EnemyLoot enemyLoot;
    private void Awake()
    {
        // Lấy các component cần thiết trên cùng GameObject
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelect = GetComponent<EnemySelect>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        // Khởi tạo máu hiện tại bằng máu gốc
        mauhientai = health;
    }

    /// <summary>
    /// Hàm nhận sát thương từ bên ngoài
    /// </summary>
    /// <param name="amount">Lượng sát thương nhận</param>
    public void TakeDamage(float amount)
    {
        // Trừ máu
        mauhientai -= amount;

        if (mauhientai <= 0f)
        {
            DisableEnemy();
        }
        else
        {
            // Nếu chưa chết thì hiển thị sát thương bay lên
            DmgManager.instance.hienSatthuong(amount, transform);
        }
    }

    private void DisableEnemy()
    {
        // Nếu chết thì bật animation chết
        animator.SetTrigger("Death");

        // Tắt AI không cho enemy di chuyển nữa
        enemyBrain.enabled = false;

        // Tắt sprite chọn enemy nếu đang hiển thị
        enemySelect.NoSelectedCallback();

        // Đổi layer để tránh bị chọn lại (Raycast sẽ bỏ qua)
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        OnEnemyDeathEvent?.Invoke();

        GameManager.instance.ThemKN(enemyLoot.ExpDrop);
    }
}
