using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Quản lý logic tấn công của người chơi, bao gồm cả tấn công phép và cận chiến.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;                  // Chứa các chỉ số của người chơi
    [SerializeField] private Weapon cacvukhi;                    // Vũ khí mặc định được trang bị ban đầu
    [SerializeField] private Transform[] vitritancong;           // Các vị trí tấn công tương ứng các hướng: 0 (trên), 1 (phải), 2 (dưới), 3 (trái)

    [Header("Cận chiến config")]
    [SerializeField] private ParticleSystem slashFx;             // Hiệu ứng chém khi đánh gần
    [SerializeField] private float khoangcachCt;                 // Khoảng cách tối đa để đánh trúng cận chiến

    public Weapon currentWp { get; private set; }                // Vũ khí hiện tại đang sử dụng

    private PlayerAction action;                                 // Input system đã cài sẵn (Custom InputActions)
    private PlayerAnimation playerAnimation;                     // Xử lý animation nhân vật
    private EnemyBrain enemyTrget;                               // Kẻ địch đang bị chọn làm mục tiêu
    private Coroutine attackCoroutine;                           // Coroutine tấn công đang hoạt động
    private PlayerMovements playerMovements;                     // Script điều khiển di chuyển người chơi
    private Transform currentAttackPosition;                     // Vị trí thực hiện tấn công (spawn projectile hoặc slash effect)
    private PlayerMana playerMana;                               // Quản lý mana và sử dụng mana
    private float currentAttackRotation;                         // Góc quay khi tạo projectile phép

    private void Awake()
    {
        // Gán các component cần thiết
        playerAnimation = GetComponent<PlayerAnimation>();
        action = new PlayerAction();
        playerMovements = GetComponent<PlayerMovements>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        EquipWeapon(cacvukhi); // Trang bị vũ khí mặc định
        action.Attack.ClickAttack.performed += ctx => Attack(); // Gán sự kiện tấn công khi input được nhận
    }

    private void Update()
    {
        GetFirePosition(); // Luôn cập nhật hướng và vị trí tấn công dựa trên hướng di chuyển
    }

    /// <summary>
    /// Bắt đầu tấn công nếu có kẻ địch được chọn.
    /// </summary>
    private void Attack()
    {
        if (enemyTrget == null) return;

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine); // Hủy coroutine cũ nếu có

        attackCoroutine = StartCoroutine(IEattack()); // Bắt đầu coroutine mới
    }

    /// <summary>
    /// Coroutine xử lý logic tấn công chính.
    /// </summary>
    private IEnumerator IEattack()
    {
        if (currentAttackPosition == null) yield break;

        if (currentWp.loaiVK == LoaiVK.Phep)
        {
            if (playerMana.luongmn < currentWp.requiredMana) yield break;
            MagicAtk(); // Tấn công bằng phép
        }
        else
        {
            Canchien(); // Tấn công cận chiến
        }

        playerAnimation.setAttackani(true); // Kích hoạt animation tấn công
        yield return new WaitForSeconds(0.5f); // Đợi một khoảng thời gian ngắn rồi tắt animation
        playerAnimation.setAttackani(false);
    }

    /// <summary>
    /// Xử lý tấn công cận chiến.
    /// </summary>
    private void Canchien()
    {
        slashFx.transform.position = currentAttackPosition.position;
        slashFx.Play(); // Bật hiệu ứng slash

        float denkethu = Vector3.Distance(enemyTrget.transform.position, transform.position);
        if (denkethu <= khoangcachCt)
            enemyTrget.GetComponent<IdamageAble>().TakeDamage(GetAtkdmg()); // Gây sát thương nếu trong phạm vi
    }

    /// <summary>
    /// Tạo và bắn đạn phép.
    /// </summary>
    private void MagicAtk()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation)); // Góc xoay
        Projectiles projectiles = Instantiate(currentWp.projectilesPrefab, currentAttackPosition.position, rotation);

        projectiles.direction = Vector3.up; // 
        projectiles.dmg = GetAtkdmg();

        playerMana.UseMana(currentWp.requiredMana); // Trừ mana
    }

    /// <summary>
    /// Tính toán sát thương với khả năng chí mạng.
    /// </summary>
    private float GetAtkdmg()
    {
        float dmg = stats.BaseDmg + currentWp.dmg;
        float randomPerc = Random.Range(0f, 100);
        if (randomPerc <= stats.CritChance)
        {
            dmg += dmg * (stats.CritDmg / 100f); // Tăng sát thương nếu chí mạng
        }
        return dmg;
    }

    /// <summary>
    /// Trang bị vũ khí mới cho người chơi.
    /// </summary>
    public void EquipWeapon(Weapon vukhimoi)
    {
        currentWp = vukhimoi;
        stats.TotalDmg = stats.BaseDmg + currentWp.dmg;
    }

    /// <summary>
    /// Cập nhật vị trí tấn công và góc xoay dựa trên hướng di chuyển.
    /// </summary>
    private void GetFirePosition()
    {
        Vector2 movedirection = playerMovements.MoveDirection;

        switch (movedirection.x)
        {
            case > 0f:
                currentAttackPosition = vitritancong[1]; // phải
                currentAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = vitritancong[3]; // trái
                currentAttackRotation = -270f;
                break;
        }

        switch (movedirection.y)
        {
            case > 0f:
                currentAttackPosition = vitritancong[0]; // lên
                currentAttackRotation = 0f;
                break;
            case < 0f:
                currentAttackPosition = vitritancong[2]; // xuống
                currentAttackRotation = -180f;
                break;
        }
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTrget = enemySelected; // Lưu lại mục tiêu đã chọn
    }

    private void NoEnemySelectionCallback()
    {
        enemyTrget = null; // Hủy mục tiêu nếu không còn chọn
    }

    private void OnEnable()
    {
        action.Enable(); // Bật input system
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent += NoEnemySelectionCallback;
    }

    private void OnDisable()
    {
        action.Disable(); // Tắt input system
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent -= NoEnemySelectionCallback;
    }
}
