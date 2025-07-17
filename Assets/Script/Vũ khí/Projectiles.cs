// Script điều khiển viên đạn bay và gây sát thương khi va chạm
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 direction { get; set; }
    public float dmg { get; set; }
    private bool isPaused;

    private void OnEnable()
    {
        PauseManager.OnPauseChanged += HandlePauseChanged;
    }

    private void OnDisable()
    {

        PauseManager.OnPauseChanged -= HandlePauseChanged;
    }

    private void HandlePauseChanged(bool paused)
    {
        isPaused = paused;
    }

    private void Update()
    {
        if (isPaused) return;
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IdamageAble>()?.TakeDamage(dmg);
        Destroy(gameObject);
    }
}