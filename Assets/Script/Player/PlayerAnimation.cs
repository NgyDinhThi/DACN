using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Hash các tham số Animator để tối ưu hiệu suất
    private readonly int moveX = Animator.StringToHash("Move_X");
    private readonly int moveY = Animator.StringToHash("Move_Y");
    private readonly int diChuyen = Animator.StringToHash("DiChuyen");

    private readonly int dead = Animator.StringToHash("Dead");

    private Animator animator; // Điều khiển Animator của nhân vật

    private void Awake()
    {
         
        animator = GetComponent<Animator>(); // Lấy thành phần Animator từ GameObject
    }

    public void SetDeadAni()
    {
        animator.SetTrigger(dead);  
    }    

    public void SetMoveBoolTransition( bool value)
    {
        animator.SetBool(diChuyen, value);
    }   
    
    public void SetMoveAni(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x); // Cập nhật giá trị X cho Animator
        animator.SetFloat(moveY, dir.y); // Cập nhật giá trị Y cho Animator
    }    
}
