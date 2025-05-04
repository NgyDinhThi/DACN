using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Weapon cacvukhi; // Vũ khí hiện tại của người chơi
    [SerializeField] private Transform[] vitritancong; // Mảng vị trí tấn công theo hướng (0: lên, 1: phải, 2: xuống, 3: trái)

    private PlayerAction action; // Script chứa input actions (được tạo bằng Input System)
    private PlayerAnimation playerAnimation; // Điều khiển animation của người chơi
    private EnemyBrain enemyTrget; // Đối tượng kẻ địch đang bị chọn
    private Coroutine attackCoroutine; // Coroutine đang chạy cho đòn tấn công
    private PlayerMovements playerMovements; // Điều khiển di chuyển người chơi
    private Transform noitancong; // Vị trí bắn hiện tại
    private PlayerMana playerMana; // Kiểm tra và tiêu hao mana khi tấn công
    private float xoayhuong; // Góc quay của viên đạn hoặc kỹ năng

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
        if (noitancong != null)
        {

            if (playerMana.luongmn < cacvukhi.luongMana) yield break;
           
            // Tạo hướng xoay cho đạn
            quaternion rotation = quaternion.Euler(new Vector3(0f, 0f, xoayhuong));

            // Khởi tạo đạn từ prefab tại vị trí tấn công và góc xoay
            Projectiles projectiles = Instantiate(cacvukhi.projectilesPrefab, noitancong.position, rotation);

            // Thiết lập hướng bay cho đạn là hướng lên (sẽ xoay theo xoayhuong)
            projectiles.huongbay = Vector3.up;

            projectiles.dmg = cacvukhi.dmg;

            // Trừ mana sau khi bắn
            playerMana.UseMana(cacvukhi.luongMana);
        }

        // Kích hoạt animation tấn công
        playerAnimation.setAttackani(true);

        // Đợi nửa giây rồi tắt animation
        yield return new WaitForSeconds(0.5f);
        playerAnimation.setAttackani(false);
    }

    private void GetFirePosition()
    {
        // Lấy hướng di chuyển của người chơi
        Vector2 movedirection = playerMovements.MoveDirection;

        // Cập nhật vị trí tấn công và hướng xoay theo trục X
        switch (movedirection.x)
        {
            case > 0f:
                noitancong = vitritancong[1]; // phải
                xoayhuong = -90f;
                break;
            case < 0f:
                noitancong = vitritancong[3]; // trái
                xoayhuong = -270f;
                break;
        }

        // Cập nhật vị trí tấn công và hướng xoay theo trục Y
        switch (movedirection.y)
        {
            case > 0f:
                noitancong = vitritancong[0]; // lên
                xoayhuong = 0f;
                break;
            case < 0f:
                noitancong = vitritancong[2]; // xuống
                xoayhuong = -180f;
                break;
        }
    }

    // Gọi khi người chơi chọn một kẻ địch
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTrget = enemySelected;
    }

    // Gọi khi người chơi bỏ chọn kẻ địch
    private void NoenemySelectionCallback()
    {
        enemyTrget = null;
    }

    private void OnEnable()
    {
        action.Enable(); // Bật input system
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoenemySelectionCallback;
    }

    private void OnDisable()
    {
        action.Disable(); // Tắt input system
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoenemySelectionCallback;
    }
}
