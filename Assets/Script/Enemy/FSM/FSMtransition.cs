using System;
using UnityEngine;

[Serializable] // Cho phép hiển thị trong Inspector nếu được sử dụng trong class MonoBehaviour hoặc ScriptableObject
public class FSMtransition
{
    public FSMdecition Quyetdinh;   // Quyết định sẽ được thực hiện (một class con của FSMdecition)
    public string TrueState;        // ID trạng thái tiếp theo nếu điều kiện (Quyetdinh.Decide()) là đúng
    public string FalseState;       // ID trạng thái tiếp theo nếu điều kiện là sai
}
