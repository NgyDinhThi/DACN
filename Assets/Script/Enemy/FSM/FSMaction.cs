using UnityEngine;

// FSMaction là một lớp trừu tượng dùng làm cơ sở cho các hành vi trong FSM (Finite State Machine)
public abstract class FSMaction : MonoBehaviour
{
    // Act là phương thức trừu tượng, buộc các lớp kế thừa phải định nghĩa cụ thể
    // Dùng để thực thi hành vi cụ thể trong mỗi trạng thái
    public abstract void Act();
}
