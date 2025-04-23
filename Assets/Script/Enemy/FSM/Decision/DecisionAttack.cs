using UnityEngine;

public class DecisionAttack : FSMdecition
{ 

    [Header("Config")]
    [SerializeField] private float khoangcachtancong;
    [SerializeField] private LayerMask playerLayerMask;

    private EnemyBrain enemy;

    public override bool Decide()
    {
        return vungtancong();
    }

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    
    private bool vungtancong()
    {
        if (enemy.nguoichoi == null)
        return false;
        Collider2D playerCollider = Physics2D.OverlapCircle(enemy.transform.position, khoangcachtancong, playerLayerMask);
        if (playerCollider != null) return true;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, khoangcachtancong);
    }

    
}
