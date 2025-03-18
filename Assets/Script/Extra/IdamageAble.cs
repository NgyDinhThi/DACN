using UnityEngine;
using System;

// Interface định nghĩa khả năng nhận sát thương cho các đối tượng
public interface IdamageAble
{
    // Phương thức bắt buộc các lớp triển khai phải có để nhận sát thương
    void TakeDamage(float amount);
}