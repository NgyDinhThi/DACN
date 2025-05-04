using UnityEngine;

public class EnemySelect : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject selectorSpritel; // Sprite dùng để hiển thị vùng chọn/quầng sáng khi enemy được chọn

    private EnemyBrain enemy; // Tham chiếu đến component EnemyBrain trên GameObject này

    private void Awake()
    {
        // Lấy EnemyBrain từ chính GameObject đính kèm script này
        enemy = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Callback khi một enemy được chọn
    /// </summary>
    /// <param name="enemySelected">Enemy được chọn</param>
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        // Nếu enemy được chọn chính là enemy hiện tại → bật sprite chọn
        if (enemySelected == enemy)
        {
            selectorSpritel.SetActive(true);
        }
        else
        {
            selectorSpritel.SetActive(false);
        }
    }

    /// <summary>
    /// Callback khi không có enemy nào được chọn
    /// </summary>
    public void NoSelectedCallback()
    {
        // Tắt sprite chọn vì không có enemy nào được chọn
        selectorSpritel.SetActive(false);
    }

    private void OnEnable()
    {
        // Đăng ký callback khi có enemy được chọn hoặc không chọn
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoSelectedCallback;
    }

    private void OnDisable()
    {
        // Gỡ đăng ký callback khi script bị disable để tránh memory leak hoặc lỗi logic
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoSelectedCallback;
    }
}
