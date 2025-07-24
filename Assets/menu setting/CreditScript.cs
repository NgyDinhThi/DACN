using UnityEngine;

public class CreditScript : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float ScrollSpeed;
    [SerializeField] private float endY;
    [SerializeField] private CreditRole role; // enum ở đây

    private RectTransform rectTransform;
    private bool hasNotified = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, ScrollSpeed * Time.deltaTime);

        if (!hasNotified && rectTransform.anchoredPosition.y >= endY)
        {
            hasNotified = true;

            // Gọi hàm theo enum
            if (role == CreditRole.TitleText)
                EndingSceneManager.Instance.NotifyTitleFinished();
            else if (role == CreditRole.CreditText)
                EndingSceneManager.Instance.NotifyCreditFinished();
            Destroy(gameObject);
        }
    }
}
