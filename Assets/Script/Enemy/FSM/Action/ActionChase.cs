using UnityEngine;

public class ActionChase : FSMaction
{
    [Header("Config")]
    
    [SerializeField] private float tocdoduoi;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        duoitheongchoi();
    }


    private void duoitheongchoi()
    {
        if (enemy.nguoichoi == null)
            return;
        Vector3 huongNgchoi = enemy.nguoichoi.position - transform.position;
        if (huongNgchoi.magnitude >= 1.5f)
        {
            transform.Translate(huongNgchoi.normalized * (tocdoduoi * Time.deltaTime));
        }
    }    
}
