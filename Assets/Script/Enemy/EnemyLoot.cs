using UnityEngine;
using UnityEngine.Rendering;

public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expdrop;


    public float Expdrop => expdrop;
}
