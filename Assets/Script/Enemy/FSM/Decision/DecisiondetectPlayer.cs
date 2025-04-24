using UnityEngine;

// DecisiondetectPlayer kế thừa từ FSMdecition: là một "quyết định" trong FSM
// Trả về true nếu enemy phát hiện thấy người chơi trong phạm vi xác định
public class DecisiondetectPlayer : FSMdecition
{
    [Header("Config")]
    [SerializeField] private float khoangcach; // Bán kính phát hiện người chơi
    [SerializeField] private LayerMask playerLayerMask; // Layer để xác định người chơi

    private EnemyBrain enemy; // Tham chiếu đến bộ não điều khiển enemy

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>(); // Lấy script EnemyBrain gắn cùng GameObject
    }

    // Hàm Decide được gọi trong FSM để xác định enemy có nên chuyển trạng thái (VD: từ Patrol sang Chase)
    public override bool Decide()
    {
        return phathienngchoi(); // Gọi hàm kiểm tra phát hiện
    }

    // Hàm kiểm tra xem có phát hiện player hay không trong vùng bán kính
    private bool phathienngchoi()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            enemy.transform.position, khoangcach, playerLayerMask
        );

        // Nếu có phát hiện, cập nhật nguoichoi và trả về true
        if (playerCollider != null)
        {
            enemy.nguoichoi = playerCollider.transform;
            return true;
        }

        // Nếu không phát hiện, reset người chơi
        enemy.nguoichoi = null;
        return false;
    }

    // Vẽ vùng phát hiện người chơi trong Scene view để dễ quan sát khi debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, khoangcach); // Vẽ hình tròn vùng phát hiện
    }
}
