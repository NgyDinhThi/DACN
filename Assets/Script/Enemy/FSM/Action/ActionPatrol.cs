using UnityEngine;

// ActionPatrol là một hành động trong FSM (Finite State Machine),
// đại diện cho trạng thái Enemy đang tuần tra theo các điểm waypoint.
public class ActionPatrol : FSMaction
{
    [Header("Config")]
    [SerializeField] private float speed; // Tốc độ di chuyển khi tuần tra

    private Waypoint WayPoint;           // Tham chiếu đến component quản lý waypoint
    private int diadiemIndex;            // Chỉ số của vị trí hiện tại trong mảng waypoint
    private Vector3 vitritieptheo;       // Biến lưu vị trí tiếp theo sẽ đi (không dùng ở đây)

    // Lấy component WayPoint khi đối tượng được tạo
    private void Awake()
    {
        WayPoint = GetComponent<Waypoint>();
    }

    // Lấy vị trí hiện tại từ WayPoint theo chỉ số
    private Vector3 vitrihientai()
    {
        return WayPoint.Layvitri(diadiemIndex);
    }

    // Hàm di chuyển tới vị trí tiếp theo
    private void followPath()
    {
        // Di chuyển dần về vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, vitrihientai(), speed * Time.deltaTime);

        // Nếu đến gần đủ vị trí hiện tại thì cập nhật sang waypoint tiếp theo
        if (Vector3.Distance(transform.position, vitrihientai()) <= 0.1f)
        {
            Capnhatvitrihientai();
        }
    }

    // Hàm cập nhật chỉ số waypoint khi đến đích
    private void Capnhatvitrihientai()
    {
        diadiemIndex++;

        // Nếu đến cuối danh sách waypoint, quay lại đầu (lặp lại)
        if (diadiemIndex > WayPoint.Diadiem.Length - 1)
        {
            diadiemIndex = 0;
        }
    }

    // Override từ FSMaction: gọi khi hành động tuần tra đang diễn ra
    public override void Act()
    {
        followPath();  // Gọi hành vi di chuyển tuần tra
    }
}
