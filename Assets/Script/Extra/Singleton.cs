using UnityEngine;

/// <summary>
/// Lớp Singleton cơ bản dùng để đảm bảo chỉ có duy nhất một instance của một class kế thừa từ MonoBehaviour.
/// </summary>
/// <typeparam name="T">Kiểu đối tượng kế thừa từ MonoBehaviour (ví dụ: GameManager, UIManager...)</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Thuộc tính tĩnh dùng để truy cập thể hiện duy nhất của lớp T
    public static T instance { get; private set; }

    /// <summary>
    /// Awake là hàm Unity gọi đầu tiên khi object được khởi tạo.
    /// Gán thể hiện hiện tại (this) vào biến static instance.
    /// </summary>
    protected virtual void Awake()
    {
        instance = this as T;
    }
}
