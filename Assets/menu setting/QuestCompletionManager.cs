using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Quản lý chuyển scene sau khi hoàn thành tất cả nhiệm vụ
/// Gọi TryCompleteQuestsAndLoadScene() sau khi nhấn nút Claim
/// </summary>
public class QuestCompletionManager : MonoBehaviour
{
    [Header("Cấu hình")]
    [SerializeField] private Quest[] allQuests;           // Các nhiệm vụ cần hoàn thành
    [SerializeField] private string sceneToLoad;          // Tên scene cần load

    private bool hasTriggered = false;

    /// <summary>
    /// Gọi khi người chơi nhấn nút Claim
    /// </summary>
    public void TryCompleteQuestsAndLoadScene()
    {
        if (hasTriggered)
            return;

        // Kiểm tra tất cả nhiệm vụ đã hoàn thành chưa
        bool allCompleted = true;
        foreach (var quest in allQuests)
        {
            if (!quest.QuestCompleted)
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            hasTriggered = true;
            Debug.Log("Tất cả nhiệm vụ đã hoàn thành! Đang chuyển scene...");

            // Kiểm tra tên scene có hợp lệ chưa
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("Chưa nhập tên Scene cần load trong QuestCompletionManager!");
            }
        }
        else
        {
            Debug.Log("Vẫn còn nhiệm vụ chưa hoàn thành!");
        }
    }
}
