using UnityEngine;

// Quản lý các animation của nhân vật (di chuyển, tấn công, chết, hồi sinh)
public class PlayerAnimation : MonoBehaviour
{
    private readonly int moveX = Animator.StringToHash("Move_X");
    private readonly int moveY = Animator.StringToHash("Move_Y");
    private readonly int diChuyen = Animator.StringToHash("DiChuyen");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int revie = Animator.StringToHash("Revie");
    private readonly int tancong = Animator.StringToHash("Attacking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Gọi animation chết
    public void SetDeadAni()
    {
        animator.SetTrigger(dead);
    }

    // Bật/tắt animation di chuyển
    public void SetMoveBoolTransition(bool value)
    {
        animator.SetBool(diChuyen, value);
    }

    // Cập nhật hướng di chuyển cho animation
    public void SetMoveAni(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }

    // Gọi animation hồi sinh
    public void ResetPlayer()
    {
        SetMoveAni(Vector2.down);
        animator.SetTrigger(revie);
    }

    // Bật/tắt animation tấn công
    public void setAttackani(bool value)
    {
        animator.SetBool(tancong, value);
    }
}
