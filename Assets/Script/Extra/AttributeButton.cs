using System;
using UnityEngine;

public class AttributeButton : MonoBehaviour
{
   public static event Action<Attribute> OnAttributeEvent;
    [Header("Config")]
    [SerializeField] private Attribute attribute;

    public void SelectAttribute()
    {
        OnAttributeEvent?.Invoke(attribute);

    }    

}
