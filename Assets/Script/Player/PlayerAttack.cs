using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapon cacvukhi; // Vũ khí hiện tại của người chơi
    [SerializeField] private Transform[] vitritancong; // Mảng vị trí tấn công theo hướng (0: lên, 1: phải, 2: xuống, 3: trái)

    [Header("Cận chiến config")]
    [SerializeField] private ParticleSystem slashFx;
    [SerializeField] private float khoangcachCt;

    public Weapon currentWp { get; private set; }

    private PlayerAction action; // Script chứa input actions (được tạo bằng Input System)
    private PlayerAnimation playerAnimation; // Điều khiển animation của người chơi
    private EnemyBrain enemyTrget; // Đối tượng kẻ địch đang bị chọn
    private Coroutine attackCoroutine; // Coroutine đang chạy cho đòn tấn công
    private PlayerMovements playerMovements; // Điều khiển di chuyển người chơi
    private Transform currentAttackPosition; // Vị trí bắn hiện tại
    private PlayerMana playerMana; // Kiểm tra và tiêu hao mana khi tấn công
    private float currentAttackRotation; // Góc quay của viên đạn hoặc kỹ năng

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
       EquipWeapon(cacvukhi);

        // Đăng ký sự kiện khi nhấn nút tấn công
        action.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        // Luôn cập nhật hướng bắn dựa trên hướng di chuyển
        GetFirePosition();
    }

    private void Attack()
    {
        if (enemyTrget == null) return; // Nếu không có mục tiêu thì không tấn công

        // Nếu đã có coroutine đang chạy thì dừng lại trước
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        // Bắt đầu coroutine tấn công mới
        attackCoroutine = StartCoroutine(IEattack());
    }

    private IEnumerator IEattack()
       // dungf IEnum nên không dùng return được

    {
        if (currentAttackPosition == null) yield break;
       

        if (currentWp.loaiVK == LoaiVK.Phep)
        {
            if (playerMana.luongmn < currentWp.requiredMana) yield break;
            MagicAtk();
        }
        else
        {
            Canchien();
        }

        // Kích hoạt animation tấn công
        playerAnimation.setAttackani(true);

        // Đợi nửa giây rồi tắt animation
        yield return new WaitForSeconds(0.5f);
        playerAnimation.setAttackani(false);
    }

    private void Canchien()
    {
        slashFx.transform.position = currentAttackPosition.position;
        slashFx.Play();
        float denkethu = Vector3.Distance(enemyTrget.transform.position, transform.position);
        if (denkethu <=khoangcachCt)
        {
            enemyTrget.GetComponent<IdamageAble>().TakeDamage(GetAtkdmg());
        }
    }    
     
    private void MagicAtk()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectiles projectiles = Instantiate(currentWp.projectilesPrefab,currentAttackPosition.position, rotation);
        projectiles.direction = Vector3.up;
        projectiles.dmg = GetAtkdmg();
        playerMana.UseMana(currentWp.requiredMana);
    }    

    private float GetAtkdmg()
    {
        float dmg = stats.BaseDmg;
        dmg += currentWp.dmg;
        float randomPerc = Random.Range(0f, 100);
        if (randomPerc <= stats.CritChance)
        {
            dmg += dmg * (stats.CritDmg / 100f);
        }
        return dmg;
    }    

    public void EquipWeapon(Weapon vukhimoi)
    {

        currentWp = vukhimoi;
        stats.TotalDmg = stats.BaseDmg + currentWp.dmg;
    }    
    private void GetFirePosition()
    {
        // Lấy hướng di chuyển của người chơi
        Vector2 movedirection = playerMovements.MoveDirection;

        // Cập nhật vị trí tấn công và hướng xoay theo trục X
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

        // Cập nhật vị trí tấn công và hướng xoay theo trục Y
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

    // Gọi khi người chơi chọn một kẻ địch
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTrget = enemySelected;
    }

    // Gọi khi người chơi bỏ chọn kẻ địch
    private void NoEnemySelectionCallback()
    {
        enemyTrget = null;
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
