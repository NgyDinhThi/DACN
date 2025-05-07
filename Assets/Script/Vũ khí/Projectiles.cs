using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed; // Tốc độ bay của đạn

    // Hướng bay của đạn, được set từ bên ngoài khi khởi tạo
    public Vector3 direction { get; set; }

    public float dmg { get; set; }  

    private void Update()
    {
        // Di chuyển đạn theo hướng đã chỉ định, với tốc độ cố định mỗi frame
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IdamageAble>()?.TakeDamage(dmg);
        // Khi đạn va chạm với bất kỳ collider nào, nó sẽ tự hủy
        Destroy(gameObject);
    }
}
