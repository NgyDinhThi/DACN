using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]

    [SerializeField] private Weapon cacvukhi;
    [SerializeField] private Transform[] vitritancong;

   private PlayerAction action;

   private PlayerAnimation playerAnimation;

   private EnemyBrain enemyTrget;

   private Coroutine attackCoroutine;

   private PlayerMovements playerMovements;

   private Transform noitancong;
       
   private PlayerMana playerMana;
    
    private float xoayhuong;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        action = new PlayerAction();
        playerMovements = GetComponent<PlayerMovements>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        action.Attack.ClickAttack.performed += ctx => Attack();    
    }

    private void Update()
    {
        GetFirePosition();   
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
        if (noitancong != null)
        {
            quaternion rotation = quaternion.Euler(new Vector3 (0f, 0f, xoayhuong));

            Projectiles projectiles = Instantiate(cacvukhi.projectilesPrefab, noitancong.position, rotation);
            projectiles.huongbay = Vector3.up;
            playerMana.UseMana(cacvukhi.luongMana);
        }


        playerAnimation.setAttackani(true);

        yield return new WaitForSeconds(0.5f);
        playerAnimation.setAttackani(false);
    }

    private void GetFirePosition()
    {
        Vector2 movedirection = playerMovements.MoveDirection;
        switch (movedirection.x)
        {
            case > 0f:
                noitancong = vitritancong[1];
                xoayhuong = -90f;
                break;
            case < 0f:
                noitancong = vitritancong[3];
                xoayhuong = -270f;

                break;
        }
        switch (movedirection.y)
        {
            case > 0f:
                noitancong = vitritancong[0];
                xoayhuong = 0f;
                break;
        
            case < 0f:
                noitancong = vitritancong[2];
                xoayhuong = -180f;

                break;
        }

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