using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3[] diadiem;  // Mảng chứa các điểm waypoint (tọa độ tương đối so với vị trí gốc)

    public Vector3[] Diadiem => diadiem; // Thuộc tính chỉ đọc để lấy danh sách waypoint

    private bool khoidonggame; // Biến kiểm tra xem game đã bắt đầu chưa (dùng cho Gizmos)

    public Vector3 entitypPosition { get; set; } // Vị trí gốc của thực thể (entity) gắn WayPoint

    private void Start()
    {
        entitypPosition = transform.position; // Ghi nhận vị trí ban đầu
        khoidonggame = true; // Đánh dấu game đã khởi động
    }

    // Lấy tọa độ vị trí thực tế từ waypoint theo chỉ số index
    public Vector3 Layvitri(int diadiemIndex)
    {
        return entitypPosition + diadiem[diadiemIndex];
    }

    // Dùng để hiển thị waypoint trong Scene khi chỉnh sửa
    private void OnDrawGizmos()
    {
        // Nếu chưa khởi động game và đối tượng đã bị thay đổi vị trí thì cập nhật vị trí gốc
        if (khoidonggame == false && transform.hasChanged)
        {
            entitypPosition = transform.position;
        }
    }
}
