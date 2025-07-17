using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapon initialweapon;
    [SerializeField] private Transform[] vitritancong;

    [Header("Cận chiến config")]
    [SerializeField] private ParticleSystem slashFx;
    [SerializeField] private float khoangcachCt;

    public Weapon currentWp { get; private set; }

    private PlayerAction action;
    private PlayerAnimation playerAnimation;
    private EnemyBrain enemyTrget;
    private Coroutine attackCoroutine;
    private PlayerMovements playerMovements;
    private Transform currentAttackPosition;
    private PlayerMana playerMana;
    private float currentAttackRotation;
    private bool isPaused;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        action = new PlayerAction();
        playerMovements = GetComponent<PlayerMovements>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        WeaponManager.instance.EquipWeapon(initialweapon);
        action.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        if (isPaused) return;
        GetFirePosition();
    }

    private void Attack()
    {
        if (isPaused) return;
        if (enemyTrget == null) return;

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(IEattack());
    }

    private IEnumerator IEattack()
    {
        if (currentAttackPosition == null) yield break;

        if (currentWp.loaiVK == LoaiVK.Phep)
        {
            if (playerMana.luongmn < currentWp.requiredMana) yield break;
            MagicAtk();
        }
        else
        {
            Canchien();
        }

        playerAnimation.setAttackani(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimation.setAttackani(false);
    }

    private void Canchien()
    {
        slashFx.transform.position = currentAttackPosition.position;
        slashFx.Play();

        float denkethu = Vector3.Distance(enemyTrget.transform.position, transform.position);
        if (denkethu <= khoangcachCt)
            enemyTrget.GetComponent<IdamageAble>()?.TakeDamage(GetAtkdmg());
    }

    private void MagicAtk()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectiles projectiles = Instantiate(currentWp.projectilesPrefab, currentAttackPosition.position, rotation);

        projectiles.direction = Vector3.up;
        projectiles.dmg = GetAtkdmg();

        playerMana.UseMana(currentWp.requiredMana);
    }

    private float GetAtkdmg()
    {
        float dmg = stats.BaseDmg + currentWp.dmg;
        float randomPerc = Random.Range(0f, 100);
        if (randomPerc <= stats.CritChance)
        {
            dmg += dmg * (stats.CritDmg / 100f);
        }
        return dmg;
    }

    public void EquipWeapon(Weapon vukhimoi)
    {
        currentWp = vukhimoi;
        stats.TotalDmg = stats.BaseDmg + currentWp.dmg;
    }

    private void GetFirePosition()
    {
        Vector2 movedirection = playerMovements.MoveDirection;

        switch (movedirection.x)
        {
            case > 0f:
                currentAttackPosition = vitritancong[1];
                currentAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = vitritancong[3];
                currentAttackRotation = -270f;
                break;
        }

        switch (movedirection.y)
        {
            case > 0f:
                currentAttackPosition = vitritancong[0];
                currentAttackRotation = 0f;
                break;
            case < 0f:
                currentAttackPosition = vitritancong[2];
                currentAttackRotation = -180f;
                break;
        }
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTrget = enemySelected;
    }

    private void NoEnemySelectionCallback()
    {
        enemyTrget = null;
    }

    private void HandlePauseChanged(bool paused)
    {
        isPaused = paused;

        // Optionally stop animation if đang đánh
        if (paused && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            playerAnimation.setAttackani(false);
        }
    }

    private void OnEnable()
    {
        action.Enable();
        PauseManager.OnPauseChanged += HandlePauseChanged;
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent += NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent += NoEnemySelectionCallback;
    }

    private void OnDisable()
    {
        action.Disable();
        PauseManager.OnPauseChanged -= HandlePauseChanged;
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallback;
        SelectionManager.OnnoselectionEvent -= NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent -= NoEnemySelectionCallback;
    }
}
