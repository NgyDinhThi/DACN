using UnityEngine;

// Kế thừa từ FSMaction (một class hành động trong hệ thống FSM của enemy)
public class Actionattack : FSMaction
{
    [Header("Config")]
    [SerializeField] private float Dmg;       // Lượng sát thương gây ra mỗi đòn đánh
    [SerializeField] private float TimeDmg;   // Khoảng thời gian giữa mỗi đòn đánh

    private EnemyBrain enemy; // Tham chiếu đến script điều khiển hành vi của kẻ địch
    private float timer;      // Bộ đếm thời gian để giới hạn tốc độ tấn công

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>(); // Gán EnemyBrain khi script được khởi tạo
    }

    // Hàm được gọi mỗi frame khi trạng thái FSM này đang active
    public override void Act()
    {
        tancongngchoi(); // Gọi hàm xử lý logic tấn công
    }

    // Hàm xử lý hành vi tấn công người chơi
    private void tancongngchoi()
    {
        // Nếu không tìm thấy người chơi, thoát hàm
        if (enemy.nguoichoi == null)
            return;

        timer -= Time.deltaTime; // Giảm timer theo thời gian thực

        if (timer <= 0f) // Khi đến lúc được phép đánh
        {
            // Lấy script xử lý sát thương của người chơi
            IdamageAble player = enemy.nguoichoi.GetComponent<IdamageAble>();

            // Gây sát thương cho người chơi
            player.TakeDamage(Dmg);

            // Reset lại thời gian chờ đòn đánh tiếp theo
            timer = TimeDmg;
        }
    }
}
