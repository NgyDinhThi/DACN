using UnityEngine;

public class ActionWander : FSMaction
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private float wanderTime;
    [SerializeField] private Vector2 moveRange;

    private Vector3 vitrihd;
    private float timer;


    private void Start()
    {
        GetNewDestination();
    }

    public override void Act()
    {
        timer -= Time.deltaTime;
        Vector3 huongdi = (vitrihd - transform.position).normalized;
        Vector3 movement = huongdi * (speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, vitrihd) >= 0.5f)
        {
            transform.Translate(movement);
        }

        if (timer <=0)
        {
            GetNewDestination();
            timer = wanderTime;
        }

    }

    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);
        vitrihd =transform.position +new Vector3(randomX, randomY);
    
    }

    private void OnDrawGizmosSelected()
    {
        if(moveRange != Vector2.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position , moveRange * 2f);
        }    
    }
}