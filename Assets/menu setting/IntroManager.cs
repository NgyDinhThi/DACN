using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public static IntroManager Instance;

    [Header("Config")]
    [SerializeField] private string nextSceneName; 

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
            Debug.Log("✅ Title & Credit đã chạy xong! Chuyển scene...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
