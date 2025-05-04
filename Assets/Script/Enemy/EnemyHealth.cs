using System;
using UnityEngine;


public class EnemyHealth : MonoBehaviour, IdamageAble
{
    [Header("Config")]
    [SerializeField] private float health;

    public float mauhientai {  get; private set; }

    private Animator animator;

    private EnemyBrain enemyBrain;

    private EnemySelect enemySelect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelect = GetComponent<EnemySelect>();
    }


    private void Start()
    {
        mauhientai = health;
    }


    public void TakeDamage(float amount)
    {
        mauhientai -= amount;
        if (mauhientai <= 0f)
        {
            animator.SetTrigger("Death");
            enemyBrain.enabled = false;
            enemySelect.NoSelectedCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            DmgManager.instance.hienSatthuong(amount, transform);
        }
    }
}