using UnityEngine;

// DecisionAttack kế thừa từ FSMdecition: là một "quyết định" trong hệ thống FSM (Finite State Machine)
public class DecisionAttack : FSMdecition
{
    [Header("Config")]
    [SerializeField] private float khoangcachtancong; // Bán kính phạm vi tấn công
    [SerializeField] private LayerMask playerLayerMask; // Layer dùng để xác định người chơi

    private EnemyBrain enemy; // Tham chiếu đến script quản lý logic của enemy

    // Hàm Decide() sẽ được FSM gọi liên tục để kiểm tra xem có nên chuyển sang trạng thái Attack không
    public override bool Decide()
    {
        return vungtancong(); // Gọi hàm kiểm tra vùng tấn công
    }

    // Awake là hàm khởi tạo đầu tiên - lấy component EnemyBrain
    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    // Kiểm tra xem người chơi có nằm trong phạm vi tấn công không
    private bool vungtancong()
    {
        if (enemy.nguoichoi == null)
            return false;

        // Dùng OverlapCircle để kiểm tra va chạm với player trong vùng tròn quanh enemy
        Collider2D playerCollider = Physics2D.OverlapCircle(enemy.transform.position, khoangcachtancong, playerLayerMask);

        return playerCollider != null;
    }

    // Vẽ Gizmo hình tròn vùng tấn công trong Scene view giúp dễ debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, khoangcachtancong);
    }
}
