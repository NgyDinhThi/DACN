using UnityEngine;

// FSMdecition là một lớp trừu tượng dùng để định nghĩa điều kiện chuyển trạng thái trong FSM (Finite State Machine)
public abstract class FSMdecition : MonoBehaviour
{
    // Decide là phương thức trừu tượng, trả về true nếu điều kiện để chuyển trạng thái được thỏa mãn
    public abstract bool Decide();
}
