using UnityEngine;

public class ActionPatrol : FSMaction
{
    [Header("Config")]
    
    [SerializeField] private float speed;
    private WayPoint WayPoint;
    private int diadiemIndex;
    private Vector3 vitritieptheo;

    private void Awake()
    {
        WayPoint = GetComponent<WayPoint>();
    }

    private Vector3 vitrihientai()
    {
        return WayPoint.Layvitri(diadiemIndex);
    }   
    
    private void followPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, vitrihientai(), speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, vitrihientai()) <= 0.1f)
        {
            Capnhatvitrihientai();
        }
    }    

    private void Capnhatvitrihientai()
    {
        diadiemIndex++;
        if (diadiemIndex > WayPoint.Diadiem.Length - 1)
        {
            diadiemIndex = 0;
        }

    }    

    public override void Act()
    {
       followPath();
    }


}
   
