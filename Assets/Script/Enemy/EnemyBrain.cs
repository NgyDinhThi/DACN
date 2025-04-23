using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    [SerializeField] private string initState;
    [SerializeField] private FSMstate[] states;
   

    public FSMstate CurrentState { get; set; }
    public Transform nguoichoi {  get; set; }

    private void Start()
    {
        ChangeState(initState);
    }

    private void Update()
    {
      
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateId)
    {
        FSMstate newState = GetState(newStateId);
        if (newState == null)
            return;
        CurrentState = newState;
    }

    private FSMstate GetState(string newStateId)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].id == newStateId)
            {
                return states[i];
            }
        }
        return null;
    }    
}