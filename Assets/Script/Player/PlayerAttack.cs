using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   private PlayerAction action;

   private PlayerAnimation playerAnimation;

   private EnemyBrain enemyTrget;

    private Coroutine attackCoroutine;


    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        action = new PlayerAction();
    }

    private void Start()
    {
        action.Attack.ClickAttack.performed += ctx => Attack();    
    }

    private void Attack()
    {
        if (enemyTrget == null) return;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEattack());

    }

    private IEnumerator IEattack()
    {

        playerAnimation.setAttackani(true);

        yield return new WaitForSeconds(0.5f);
        playerAnimation.setAttackani(false);
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTrget = enemySelected;

    }

    private void NoenemySelectionCallback()
    {
        enemyTrget = null;
    }

    private void OnEnable()
    {
        action.Enable();
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoenemySelectionCallback;
    }

    private void OnDisable()
    {
        action.Disable();
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoenemySelectionCallback;
    }
}