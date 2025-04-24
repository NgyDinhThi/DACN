using UnityEngine;

// Lớp EnemyBrain điều khiển hành vi của kẻ thù thông qua Finite State Machine (FSM).
public class EnemyBrain : MonoBehaviour
{
    // SerializeField cho phép chỉnh sửa giá trị trong Unity Inspector mà vẫn giữ biến private.
    // initState: ID của trạng thái khởi tạo ban đầu cho kẻ thù (ví dụ: "Idle", "Patrol").
    [SerializeField] private string initState;

    // states: Mảng chứa các trạng thái FSM (Finite State Machine) của kẻ thù.
    // Mỗi trạng thái (FSMstate) định nghĩa hành vi và chuyển đổi của kẻ thù.
    [SerializeField] private FSMstate[] states;

    // CurrentState: Thuộc tính lưu trạng thái hiện tại của kẻ thù.
    // get; set; cho phép truy cập và thay đổi trạng thái từ các lớp khác.
    public FSMstate CurrentState { get; set; }

    // nguoichoi: Thuộc tính lưu tham chiếu đến Transform của nhân vật người chơi.
    // Dùng để kẻ thù tương tác với người chơi (ví dụ: đuổi theo, tấn công).
    public Transform nguoichoi { get; set; }

    // Start: Hàm được gọi khi GameObject khởi tạo, trước khung hình đầu tiên.
    private void Start()
    {
        // Chuyển kẻ thù sang trạng thái khởi tạo (initState) khi bắt đầu game.
        ChangeState(initState);
    }

    // Update: Hàm được gọi mỗi khung hình để cập nhật hành vi của kẻ thù.
    private void Update()
    {
        // Gọi UpdateState của trạng thái hiện tại để thực thi hành vi (nếu CurrentState không null).
        // Toán tử ?. (null-conditional) ngăn lỗi nếu CurrentState là null.
        CurrentState?.UpdateState(this);
    }

    // ChangeState: Phương thức chuyển đổi trạng thái của kẻ thù sang trạng thái mới dựa trên ID.
    public void ChangeState(string newStateId)
    {
        // Tìm trạng thái mới dựa trên newStateId.
        FSMstate newState = GetState(newStateId);

        // Nếu không tìm thấy trạng thái mới, thoát hàm (không thay đổi trạng thái).
        if (newState == null)
            return;

        // Cập nhật CurrentState sang trạng thái mới.
        CurrentState = newState;
    }

    // GetState: Phương thức tìm và trả về FSMstate dựa trên ID trạng thái.
    private FSMstate GetState(string newStateId)
    {
        // Duyệt qua mảng states để tìm trạng thái có ID khớp với newStateId.
        for (int i = 0; i < states.Length; i++)
        {
            // Nếu tìm thấy trạng thái có ID khớp, trả về trạng thái đó.
            if (states[i].id == newStateId)
            {
                return states[i];
            }
        }

        // Nếu không tìm thấy trạng thái, trả về null.
        return null;
    }
}