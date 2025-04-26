using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Hash các tham số Animator để tối ưu hiệu suất
    private readonly int moveX = Animator.StringToHash("Move_X"); // Tham số trục X cho Animator
    private readonly int moveY = Animator.StringToHash("Move_Y"); // Tham số trục Y cho Animator
    private readonly int diChuyen = Animator.StringToHash("DiChuyen"); // Tham số boolean cho trạng thái di chuyển
    private readonly int dead = Animator.StringToHash("Dead"); // Tham số trigger cho trạng thái chết
    private readonly int revie = Animator.StringToHash("Revie"); // Tham số trigger cho trạng thái hồi sinh
    private readonly int tancong = Animator.StringToHash("Attacking");


    private Animator animator; // Điều khiển Animator của nhân vật

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Lấy thành phần Animator từ GameObject
    }

    // Kích hoạt animation chết của nhân vật
    public void SetDeadAni()
    {
        animator.SetTrigger(dead);
    }

    // Đặt trạng thái di chuyển (bật/tắt animation di chuyển)
    public void SetMoveBoolTransition(bool value)
    {
        animator.SetBool(diChuyen, value);
    }

    // Cập nhật animation di chuyển theo hướng
    public void SetMoveAni(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x); // Cập nhật giá trị X cho Animator
        animator.SetFloat(moveY, dir.y); // Cập nhật giá trị Y cho Animator
    }

    // Reset animation nhân vật về trạng thái mặc định khi hồi sinh
    public void ResetPlayer()
    {
        SetMoveAni(Vector2.down); // Đặt hướng di chuyển mặc định là xuống dưới
        animator.SetTrigger(revie); // Kích hoạt animation hồi sinh
    }

    public void setAttackani(bool value)
    {
        animator.SetBool(tancong, value);       
    
    }
}
