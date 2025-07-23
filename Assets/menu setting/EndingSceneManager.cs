using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    public static EndingSceneManager Instance;

    [SerializeField] private GameObject announceMenu;

    private bool isTitleDone = false;
    private bool isCreditDone = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Gọi từ TitleText
    public void NotifyTitleFinished()
    {
        isTitleDone = true;
        CheckFinish();
    }

    // Gọi từ CreditText
    public void NotifyCreditFinished()
    {
        isCreditDone = true;
        CheckFinish();
    }

    // Kiểm tra nếu cả hai đã xong
    private void CheckFinish()
    {
        if (isTitleDone && isCreditDone)
        {
            Debug.Log("✅ Title & Credit đã chạy xong! Hiện announceMenu");
            announceMenu.SetActive(true);
        }
    }
}
