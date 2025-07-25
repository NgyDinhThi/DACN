using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Quản lý hệ thống hội thoại với NPC, bao gồm hiển thị, điều hướng và tương tác phụ
public class DialogManager : Singleton<DialogManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcdialogTMP;

    public NPCinteraction npcSelected { get; set; }

    private bool dialogStarted;
    private PlayerAction actions;
    private Queue<string> dialogQueue = new Queue<string>();
    public bool IsDialogActive => dialogPanel != null && dialogPanel.activeSelf;

    // Khởi tạo input và giữ lại object khi chuyển scene
    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerAction();
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    // Đăng ký input cho hội thoại
    private void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialog();
        actions.Dialogue.Continue.performed += ctx => ContinueDialog();
    }

    // Tải các dòng thoại từ NPC đã chọn
    private void LoadDialogFromNPC()
    {
        if (npcSelected == null || npcSelected.DialogToShow == null || npcSelected.DialogToShow.dialogue.Length <= 0) return;

        foreach (string sentence in npcSelected.DialogToShow.dialogue)
            dialogQueue.Enqueue(sentence);
    }

    // Hiển thị panel hội thoại và câu chào đầu tiên
    private void ShowDialog()
    {
        if (npcSelected == null || dialogStarted || dialogPanel == null) return;

        dialogPanel.SetActive(true);
        LoadDialogFromNPC();
        npcIcon.sprite = npcSelected.DialogToShow.Icon;
        npcNameTMP.text = npcSelected.DialogToShow.Name;
        npcdialogTMP.text = npcSelected.DialogToShow.Greeting;
        dialogStarted = true;
    }

    // Đóng panel hội thoại và xoá dữ liệu hàng đợi
    public void CloseDialogPanel()
    {
        if (dialogPanel == null)
        {
            dialogStarted = false;
            dialogQueue.Clear();
            return;
        }

        dialogPanel.SetActive(false);
        dialogStarted = false;
        dialogQueue.Clear();
    }

    // Hiển thị câu thoại tiếp theo, hoặc kích hoạt tương tác nếu hết thoại
    private void ContinueDialog()
    {
        if (npcSelected == null)
        {
            dialogQueue.Clear();
            return;
        }

        if (dialogQueue.Count <= 0)
        {
            CloseDialogPanel();
            dialogStarted = false;

            if (npcSelected.DialogToShow.HasInteraction)
                OnExtraInteractionEvent?.Invoke(npcSelected.DialogToShow.InteractionType);

            return;
        }

        npcdialogTMP.text = dialogQueue.Dequeue();
    }

    // Kích hoạt input
    private void OnEnable()
    {
        actions.Enable();
    }

    // Tắt input
    private void OnDisable()
    {
        actions.Disable();
    }
}
