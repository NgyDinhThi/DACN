using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private int jump;


    private PlayerAction action;
    private Rigidbody2D rb2d;
    
    private Vector2 moveDirection;



    private void Awake()
    {
        action = new PlayerAction();
        rb2d = GetComponent<Rigidbody2D>();
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
