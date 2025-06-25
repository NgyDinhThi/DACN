using UnityEngine;
using System;
// Điều khiển di chuyển và animation của nhân vật
public class PlayerMovements : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private int jump;//chưa sử dụng đến

    public Vector2 MoveDirection => moveDirection;

    private PlayerAction action;
    private Rigidbody2D rb2d;
    private PlayerAnimation playerAnimation;
    private Player player;
    private Vector2 moveDirection;

    private void Awake()
    {
        // Khởi tạo các thành phần liên quan
        action = new PlayerAction();
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        // Xử lý di chuyển vật lý
        move();
    }

    private void Update()
    {
        // Đọc input từ người chơi
        ReadMovement();
    }

    private void move()
    {
        // Di chuyển nhân vật
        if (player.Stats.health <= 0) return;
        rb2d.MovePosition(rb2d.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        // Cập nhật hướng di chuyển và animation
        moveDirection = action.Movement.Move.ReadValue<Vector2>().normalized;

        if (moveDirection == Vector2.zero)
        {
            playerAnimation.SetMoveBoolTransition(false);
            return;
        }

        playerAnimation.SetMoveBoolTransition(true);
        playerAnimation.SetMoveAni(moveDirection);
    }

    private void OnEnable()
    {
        // Bật input system
        action.Enable();
    }

    private void OnDisable()
    {
        // Tắt input system
        action?.Disable();
    }
}
