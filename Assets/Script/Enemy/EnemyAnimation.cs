using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PauseManager.OnPauseChanged += HandlePauseChanged;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseChanged -= HandlePauseChanged;
    }

    private void HandlePauseChanged(bool isPaused)
    {
        animator.speed = isPaused ? 0f : 1f;
    }

    public void SetMove(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Die()
    {
        animator.SetTrigger("die");
    }
}
