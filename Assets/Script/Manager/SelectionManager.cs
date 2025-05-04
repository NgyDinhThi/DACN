using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectEvent;
    public static event Action OnnoselectionEvent;


    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;  
    }

    private void Update()
    {
        Chonkethu();
    }

    private void Chonkethu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyMask);

            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemy == null) return;
                if (enemyHealth.mauhientai <= 0f) return;
                    OnEnemySelectEvent?.Invoke(enemy);
                
               
            }
            else
            {
                OnnoselectionEvent?.Invoke();
            }

        }

        
    }    
}
