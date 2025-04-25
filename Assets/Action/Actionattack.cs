using UnityEngine;

public class Actionattack : FSMaction
{
    [Header("Config")]
    [SerializeField] private float Dmg;
    [SerializeField] private float TimeDmg;



    private EnemyBrain enemy;
    private float timer;


    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        tancongngchoi();
    }

    private void tancongngchoi()
    {
        if (enemy.nguoichoi == null)
            return;

        timer -= Time.deltaTime;
        if (timer <=0f)
        {
            IdamageAble player = enemy.nguoichoi.GetComponent<IdamageAble>();
            
            player.TakeDamage(Dmg);
            timer = TimeDmg;
        }
    }
}
