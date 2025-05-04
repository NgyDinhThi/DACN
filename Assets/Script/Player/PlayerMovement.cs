using UnityEngine;
using System;

public class PlayerMovements : MonoBehaviour
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private float speed; // Tốc độ di chuyển của nhân vật
    [SerializeField] private int jump; // Số lần nhảy (chưa được sử dụng trong code)

    public Vector2 MoveDirection => moveDirection;



    private PlayerAction action; // Đối tượng xử lý input của người chơi
    private Rigidbody2D rb2d; // Thành phần vật lý Rigidbody2D để di chuyển nhân vật
    private PlayerAnimation playerAnimation;
    private Player player;
    private Vector2 moveDirection; // Hướng di chuyển của nhân vật

    private void Awake()
    {
        action = new PlayerAction(); // Khởi tạo hệ thống nhập liệu của người chơi
        rb2d = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D từ GameObject
        playerAnimation = GetComponent<PlayerAnimation>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        move(); // Gọi hàm di chuyển mỗi frame vật lý
    }

    private void Update()
    {
        ReadMovement(); // Đọc input mỗi frame
    }

    private void move()
    {
        if (player.Stats.health <= 0)
        {
            return;
        }
        // Di chuyển nhân vật bằng cách thay đổi vị trí Rigidbody2D dựa trên hướng di chuyển
        rb2d.MovePosition(rb2d.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        // Đọc giá trị từ hệ thống nhập liệu và chuẩn hóa hướng di chuyển
        moveDirection = action.Movement.Move.ReadValue<Vector2>().normalized;

        if (moveDirection == Vector2.zero) // Nếu không có input
        {
            // Dừng animation di chuyển
            playerAnimation.SetMoveBoolTransition(false);
            return;
        }

        playerAnimation.SetMoveBoolTransition(true); // Kích hoạt animation di chuyển
        playerAnimation.SetMoveAni(moveDirection);// Cập nhật hướng di chuyển
    }

    private void OnEnable()
    {
        action.Enable(); // Bật hệ thống nhập liệu khi GameObject được kích hoạt
    }

    private void OnDisable()
    {
        action?.Disable(); // Vô hiệu hóa hệ thống nhập liệu khi GameObject bị tắt
    }
}
