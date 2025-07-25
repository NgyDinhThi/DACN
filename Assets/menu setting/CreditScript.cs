using UnityEngine;

public class CreditScript : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float ScrollSpeed;
    [SerializeField] private float endY;
    [SerializeField] private CreditRole role; // enum: TitleText, CreditText

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

            // Gọi đến cả IntroManager hoặc EndingSceneManager tùy scene
            switch (role)
            {
                case CreditRole.TitleText:
                    if (IntroManager.Instance != null)
                        IntroManager.Instance.NotifyTitleFinished();
                    else if (EndingSceneManager.Instance != null)
                        EndingSceneManager.Instance.NotifyTitleFinished();
                    break;

                case CreditRole.CreditText:
                    if (IntroManager.Instance != null)
                        IntroManager.Instance.NotifyCreditFinished();
                    else if (EndingSceneManager.Instance != null)
                        EndingSceneManager.Instance.NotifyCreditFinished();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
