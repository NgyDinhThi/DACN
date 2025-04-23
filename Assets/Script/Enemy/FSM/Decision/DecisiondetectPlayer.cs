using UnityEngine;

public class DecisiondetectPlayer : FSMdecition
{


    [Header("Config")]
    [SerializeField] private float khoangcach;
    [SerializeField] private LayerMask playerLayerMask;

    private EnemyBrain enemy;


    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return phathienngchoi();
    }

    private bool phathienngchoi()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(enemy.transform.position, khoangcach, playerLayerMask);
        if (playerCollider != null)
        {
            enemy.nguoichoi = playerCollider.transform;
            return true;
        }
        enemy.nguoichoi = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, khoangcach);
    }
}
