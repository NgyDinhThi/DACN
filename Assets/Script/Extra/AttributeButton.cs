using System;
using UnityEngine;

public class AttributeButton : MonoBehaviour
{
    public static event Action<Attribute> OnAttributeEvent; 

    [Header("Config")]
    [SerializeField] private Attribute attribute; 

    // Phương thức được gọi khi người dùng nhấn nút, kích hoạt sự kiện với thuộc tính được chọn
    public void SelectAttribute()
    {
        OnAttributeEvent?.Invoke(attribute); // Gọi sự kiện và truyền thuộc tính
    }
}
