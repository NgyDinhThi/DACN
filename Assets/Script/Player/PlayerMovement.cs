using UnityEngine;
using System;

public class PlayerMovements : MonoBehaviour
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private float speed; // Tốc độ di chuyển của nhân vật
    [SerializeField] private int jump; // Số lần nhảy (chưa được sử dụng trong code)

    public Vector2 MoveDirection => moveDirection; // Property trả về hướng di chuyển hiện tại

    private PlayerAction action; // Đối tượng xử lý input của người chơi (Input System)
    private Rigidbody2D rb2d; // Thành phần vật lý Rigidbody2D để xử lý di chuyển vật lý
    private PlayerAnimation playerAnimation; // Điều khiển animation của nhân vật
    private Player player; // Script chứa thông tin nhân vật (máu, năng lượng...)
    private Vector2 moveDirection; // Biến lưu hướng di chuyển (từ Input System)

    private void Awake()
    {
        action = new PlayerAction(); // Khởi tạo input action (sử dụng Unity Input System)
        rb2d = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D từ GameObject
        playerAnimation = GetComponent<PlayerAnimation>(); // Lấy script animation
        player = GetComponent<Player>(); // Lấy script thông tin nhân vật
    }

    private void FixedUpdate()
    {
        move(); // Gọi hàm di chuyển trong mỗi frame vật lý
    }

    private void Update()
    {
        ReadMovement(); // Đọc input từ người chơi mỗi frame
    }

    // Hàm xử lý logic di chuyển
    private void move()
    {
        // Nếu nhân vật đã chết (máu <= 0), không cho phép di chuyển nữa
        if (player.Stats.health <= 0)
        {
            return;
        }

        // Di chuyển bằng cách cập nhật vị trí của Rigidbody2D dựa trên hướng di chuyển
        rb2d.MovePosition(rb2d.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    // Hàm đọc hướng di chuyển từ input
    private void ReadMovement()
    {
        // Đọc giá trị hướng di chuyển từ PlayerAction (Input System) và chuẩn hóa
        moveDirection = action.Movement.Move.ReadValue<Vector2>().normalized;

        if (moveDirection == Vector2.zero) // Nếu không có input nào
        {
            playerAnimation.SetMoveBoolTransition(false); // Dừng animation di chuyển
            return;
        }

        playerAnimation.SetMoveBoolTransition(true); // Bật animation di chuyển
        playerAnimation.SetMoveAni(moveDirection);   // Cập nhật hướng cho animation
    }

    // Kích hoạt hệ thống nhập liệu khi GameObject được bật
    private void OnEnable()
    {
        action.Enable();
    }

    // Tắt hệ thống nhập liệu khi GameObject bị vô hiệu hóa
    private void OnDisable()
    {
        action?.Disable(); // Kiểm tra null trước khi gọi Disable
    }
}
