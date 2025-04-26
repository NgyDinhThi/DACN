using System;
using UnityEngine;

public class DmgManager : MonoBehaviour
{
    public static DmgManager instance;

    [Header("Config")]
    [SerializeField] private DmgText dmgTextPrefab; // Đặt tên lại cho rõ

    private void Awake()
    {
        instance = this;
    }

    public void hienSatthuong(float soSatthuong, Transform parent)
    {
        // 1. Instantiate prefab dmgText
        DmgText textInstance = Instantiate(dmgTextPrefab, parent);
        textInstance.transform.position += Vector3.right * .5f;
        textInstance.textsatthuong(soSatthuong);
        
    }
}
