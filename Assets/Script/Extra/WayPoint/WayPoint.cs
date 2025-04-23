using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3[] diadiem;

    public Vector3[] Diadiem => diadiem;

    private bool khoidonggame;

    public Vector3 entitypPosition {  get; set; }

    private void Start()
    {
        entitypPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (khoidonggame == false && transform.hasChanged)
        {
            entitypPosition = transform.position;
        }
    }
}
