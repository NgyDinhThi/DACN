using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest")]
public class Quest : ScriptableObject 
{
    [Header("Info")]
    public string Name;
    public string ID;
    public int QuestGoal;

    [Header("Description")]
    [TextArea]public string Description;

    [Header("Reward")]
    public int GoldReWard;
    public float ExpReward;
    public QuestItemReward ItemReward;

       
    [HideInInspector] public int CurrentStatus;
    [HideInInspector] public bool QuestCompleted;
    [HideInInspector]public bool QuestAccepted;

}

[Serializable]
public class QuestItemReward
{
    public InventoryItems Items;
    public int Quantity;

}