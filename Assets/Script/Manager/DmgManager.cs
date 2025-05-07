using System;
using UnityEngine;

public class DmgManager : Singleton<DmgManager>
{
    // Singleton: cho phép gọi DmgManager.instance từ nơi khác trong game
   

    [Header("Config")]
    [SerializeField] private DmgText dmgTextPrefab; // Prefab dùng để hiển thị số sát thương trên màn hình

    

    /* 
     <summary>
     Hiển thị sát thương lên vị trí gần object (ví dụ: người chơi hoặc enemy)
     </summary>
      name="soSatthuong">Giá trị sát thương muốn hiển thị
     name="parent">Transform của đối tượng bị sát thương
    */
    public void hienSatthuong(float soSatthuong, Transform parent)
    {
        // Tạo một bản sao của prefab tại vị trí cha (transform của đối tượng nhận sát thương)
        DmgText textInstance = Instantiate(dmgTextPrefab, parent);

        // Dời vị trí text một chút sang bên phải để không che khuất nhân vật
        textInstance.transform.position += Vector3.right * 0.5f;

        // Gọi hàm hiển thị số sát thương trong prefab
        textInstance.textsatthuong(soSatthuong);
    }
}
