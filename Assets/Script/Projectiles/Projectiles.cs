using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 huongbay {  get; set; }

    private void Update()
    {
        transform.Translate(huongbay *(speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("va chạm nè");
    }
}
