using UnityEngine;

public class NPCAnimation : MonoBehaviour
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

    public void SetMoving(bool isMoving)
    {
        // Set speed = 1 nếu có chuyển động, hoặc 0 nếu không
        animator.speed = isMoving ? 1f : 0f;
    }
}
