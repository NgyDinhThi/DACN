// Hiển thị số sát thương dưới dạng text nổi khi nhân vật bị trúng đòn
using TMPro;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI dmgtext;

    // Gán giá trị sát thương vào text UI
    public void textsatthuong(float dmg)
    {
        dmgtext.text = dmg.ToString();
    }

    // Hủy text khỏi scene
    public void huyText()
    {
        Destroy(gameObject);
    }
}