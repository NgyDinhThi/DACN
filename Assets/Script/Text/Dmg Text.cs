using TMPro;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI dmgtext; // Text hiển thị lượng sát thương

    // Hàm để hiển thị sát thương lên màn hình
    public void textsatthuong(float dmg)
    {
        dmgtext.text = dmg.ToString(); // Gán số sát thương vào UI
    }

    // Hàm để hủy object này sau khi hiển thị xong (ví dụ gọi từ animation hoặc timer)
    public void huyText()
    {
        Destroy(gameObject); // Xóa text khỏi scene
    }
}
