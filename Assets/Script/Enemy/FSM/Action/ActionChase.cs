using UnityEngine;

// Lớp ActionChase dùng để định nghĩa **hành động đuổi theo người chơi**
// Đây là một phần của hệ thống FSM (Finite State Machine) – máy trạng thái cho AI Enemy
public class ActionChase : FSMaction
{
    [Header("Config")]
    [SerializeField] private float tocdoduoi;  // Tốc độ đuổi theo của enemy

    private EnemyBrain enemy;  // Tham chiếu đến script điều khiển Enemy

    // Lấy component EnemyBrain khi khởi tạo
    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    // Override hàm Act() từ FSMaction – sẽ được gọi mỗi frame khi Enemy ở trạng thái Chase
    public override void Act()
    {
        duoitheongchoi();  // Gọi hàm xử lý hành vi đuổi theo người chơi
    }

    // Hàm thực hiện việc đuổi theo người chơi
    private void duoitheongchoi()
    {
        // Nếu không phát hiện được người chơi thì không làm gì
        if (enemy.nguoichoi == null)
            return;

        // Tính vector hướng từ Enemy đến người chơi
        Vector3 huongNgchoi = enemy.nguoichoi.position - transform.position;

        // Nếu khoảng cách đủ xa (lớn hơn 1.5f) thì Enemy sẽ di chuyển về phía người chơi
        if (huongNgchoi.magnitude >= 1.5f)
        {
            // Dịch chuyển Enemy theo hướng về phía người chơi, có nhân tốc độ và thời gian
            transform.Translate(huongNgchoi.normalized * (tocdoduoi * Time.deltaTime));
        }
    }
}
