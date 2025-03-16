using UnityEngine;
using System;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private int jump;


    private readonly int moveX = Animator.StringToHash("Move_X");
    private readonly int moveY = Animator.StringToHash("Move_Y");

    private PlayerAction action;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector2 moveDirection;



    private void Awake()
    {
        action = new PlayerAction();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        move();
    }
    private void Update()
    {
        ReadMovement();
    }

    private void move()
    {
        rb2d.MovePosition(rb2d.position + moveDirection * (speed * Time.fixedDeltaTime));
    }    

    private void ReadMovement()
    { 
        moveDirection = action.Movement.Move.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero)
        {
            return;
        }
        animator.SetFloat(moveX, moveDirection.x);
        animator.SetFloat(moveY, moveDirection.y);
    }    

    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action?.Disable();  
    }
    

}
