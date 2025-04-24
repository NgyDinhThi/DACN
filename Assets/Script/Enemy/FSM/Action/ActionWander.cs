using UnityEngine;

// ActionWander là một hành động trong FSM (Finite State Machine),
// đại diện cho hành vi NPC di chuyển ngẫu nhiên trong một khu vực cho phép.
public class ActionWander : FSMaction
{
    [Header("Config")]
    [SerializeField] private float speed;          // Tốc độ di chuyển
    [SerializeField] private float wanderTime;     // Thời gian sau mỗi lần chọn vị trí mới
    [SerializeField] private Vector2 moveRange;    // Phạm vi di chuyển ngẫu nhiên từ vị trí hiện tại

    private Vector3 vitrihd;   // Vị trí hiện tại NPC sẽ đi tới
    private float timer;       // Bộ đếm thời gian để đổi hướng

    // Hàm Start được gọi khi script bắt đầu chạy
    private void Start()
    {
        GetNewDestination();   // Lấy vị trí ngẫu nhiên ban đầu
    }

    // Hàm Act được gọi liên tục khi NPC ở trạng thái "Wander"
    public override void Act()
    {
        timer -= Time.deltaTime; // Giảm thời gian đếm

        // Tính toán hướng di chuyển đến đích
        Vector3 huongdi = (vitrihd - transform.position).normalized;
        Vector3 movement = huongdi * (speed * Time.deltaTime);

        // Nếu chưa đến gần vị trí đích thì tiếp tục di chuyển
        if (Vector3.Distance(transform.position, vitrihd) >= 0.5f)
        {
            transform.Translate(movement); // Di chuyển NPC
        }

        // Nếu hết thời gian wander thì chọn điểm đích mới
        if (timer <= 0)
        {
            GetNewDestination();
            timer = wanderTime;
        }
    }

    // Hàm lấy một vị trí đích mới ngẫu nhiên trong phạm vi cho phép
    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);
        vitrihd = transform.position + new Vector3(randomX, randomY);
    }

    // Hàm giúp vẽ phạm vi wander trong Scene view của Unity để dễ debug
    private void OnDrawGizmosSelected()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f); // Nhân 2 vì Range là bán kính
        }
    }
}
