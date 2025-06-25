using Unity.Cinemachine;
using UnityEngine;

// Quản lý tương tác giữa NPC và người chơi khi tiếp cận vùng va chạm (trigger)
public class NPCinteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private NPCdialog dialogToShow;           // Đối thoại tương ứng với NPC
    [SerializeField] private GameObject interactionBox;        // UI hiển thị biểu tượng tương tác

    public NPCdialog DialogToShow => dialogToShow;             // Truy cập dữ liệu hội thoại

    private bool dialogStarted;

    // Kích hoạt khi người chơi đi vào vùng tương tác
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.instance.npcSelected = this;         // Gán NPC hiện tại vào DialogManager
            interactionBox.SetActive(true);                    // Hiện khung tương tác
        }
    }

    // Kích hoạt khi người chơi rời khỏi vùng tương tác
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.instance.npcSelected = null;         // Bỏ chọn NPC
            DialogManager.instance.CloseDialogPanel();         // Tắt panel hội thoại nếu có
            interactionBox.SetActive(false);                   // Ẩn khung tương tác
        }
    }
}
