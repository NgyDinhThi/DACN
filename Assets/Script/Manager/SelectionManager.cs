using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Sự kiện khi chọn enemy thành công: truyền đối tượng EnemyBrain đã chọn
    public static event Action<EnemyBrain> OnEnemySelectEvent;

    // Sự kiện khi không chọn trúng enemy nào (click ra chỗ trống)
    public static event Action OnnoselectionEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask; // Lớp LayerMask dùng để lọc va chạm chỉ với đối tượng enemy

    private Camera mainCam; // Biến tham chiếu đến camera chính

    private void Awake()
    {
        mainCam = Camera.main; // Gán camera chính cho biến mainCam
    }

    private void Update()
    {
        Chonkethu(); // Gọi hàm kiểm tra chọn kẻ thù mỗi khung hình
    }

    private void Chonkethu()
    {
        // Nếu nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Tạo ray từ vị trí chuột đến thế giới 2D để phát hiện va chạm
            RaycastHit2D hit = Physics2D.Raycast(
                mainCam.ScreenToWorldPoint(Input.mousePosition), // Vị trí chuột trên màn hình chuyển sang thế giới
                Vector2.zero,                                    // Hướng ray là zero vì chỉ cần điểm bắn
                Mathf.Infinity,                                  // Khoảng cách không giới hạn
                enemyMask                                        // Chỉ va chạm với layer enemy
            );

            // Nếu raycast trúng collider
            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>(); // Lấy script EnemyBrain từ đối tượng bị trúng
                EnemyHealth enemyHealth = enemy?.GetComponent<EnemyHealth>(); // Lấy máu nếu có EnemyBrain

                // Nếu không có EnemyBrain hoặc máu <= 0 thì không làm gì
                if (enemy == null || enemyHealth == null || enemyHealth.mauhientai <= 0f) 
                {
                    EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                    LootManager.instance.ShowLoot(enemyLoot);
                
                }
                else
                {
                    // Gửi sự kiện chọn enemy
                    OnEnemySelectEvent?.Invoke(enemy);
                }


            }
            else
            {
                // Nếu không trúng ai -> gửi sự kiện không chọn gì
                OnnoselectionEvent?.Invoke();
            }
        }
    }
}
 