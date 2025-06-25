using TMPro;
using UnityEngine;

// Hiển thị thông tin cơ bản của nhiệm vụ trong giao diện
public class QuestCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI questNameTMP;
    [SerializeField] private TextMeshProUGUI questDescriptionTMP;

    public Quest QuestToComplete { get; set; }

    // Cấu hình giao diện nhiệm vụ với tên và mô tả
    public virtual void ConfigQuestUI(Quest quest)
    {
        QuestToComplete = quest;
        questNameTMP.text = quest.Name;
        questDescriptionTMP.text = quest.Description;
    }
}