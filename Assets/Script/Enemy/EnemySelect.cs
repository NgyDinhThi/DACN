using UnityEngine;


public class EnemySelect : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject selectorSpritel;


    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        if (enemySelected == enemy)
        {
            selectorSpritel.SetActive(true);
        }
        else
        {
            selectorSpritel.SetActive(false);
            
        }

    }    

    private void NoSelectedCallback()
    {
        selectorSpritel.SetActive(false);
    }    

    private void OnEnable()
    {
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoSelectedCallback;
    }

    private void OnDisable()
    {
        
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoSelectedCallback;

    }
}