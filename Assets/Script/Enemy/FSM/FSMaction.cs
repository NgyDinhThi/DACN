using UnityEngine;

// FSMaction là một lớp trừu tượng dùng làm cơ sở cho các hành vi trong FSM (Finite State Machine)
public abstract class FSMaction : MonoBehaviour
{
    // Act là phương thức trừu tượng, buộc các lớp kế thừa phải định nghĩa cụ thể
    // Dùng để thực thi hành vi cụ thể trong mỗi trạng thái
    protected bool isPaused;

    protected virtual void OnEnable()
    {
        PauseManager.OnPauseChanged += HandlePauseChanged;
    }

    protected virtual void OnDisable()
    {
        PauseManager.OnPauseChanged -= HandlePauseChanged;
    }

    private void HandlePauseChanged(bool paused)
    {
        isPaused = paused;
    }
    public abstract void Act();
}
