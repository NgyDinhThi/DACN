using System;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    public static event Action OnplayerUpgrade;

    [Header("Config")]
    [SerializeField] private PlayerStats stats;


    [Header("Setting")]
    [SerializeField] private UpgradeSetting[] settings;

    private void UpgradePlayer(int upgradeIndex)
    {
        stats.BaseDmg += settings[upgradeIndex].DmgUpgrade;
        stats.TotalDmg += settings[upgradeIndex].DmgUpgrade;
        stats.Max_health += settings[upgradeIndex].HealthUpgrade;
        stats.health = stats.Max_health;
        stats.Max_mana += settings[upgradeIndex].ManaUpgrade;
        stats.mana = stats.Max_mana;
        stats.CritChance += settings[upgradeIndex].CritchanceUpgrade;
        stats.CritDmg += settings[upgradeIndex].CritdmgUpgrade;
 
    }

    private void AttributeCallback(Attribute attribute)
    {
        if (stats.AttributePoint == 0) return;
        switch (attribute)
        {
          case Attribute.Strength:
                UpgradePlayer(0);
                stats.Strength++;
                break;
          case Attribute.Dexterity:
                UpgradePlayer(1);
                stats.Dexterity++;
                break;
          case Attribute.Intelligence:
                UpgradePlayer(2);
                stats.Intelligence++;
                break;
        }

        stats.AttributePoint--;
        OnplayerUpgrade?.Invoke();
    }    

    private void OnEnable()
    {
        AttributeButton.OnAttributeEvent += AttributeCallback;
    }

    private void OnDisable()
    {
        AttributeButton.OnAttributeEvent -= AttributeCallback;
         
    }
}

[Serializable]

public class UpgradeSetting
{
    public string Name;

    [Header("Value")]
    public float DmgUpgrade;
    public float HealthUpgrade;
    public float ManaUpgrade;
    public float CritchanceUpgrade;
    public float CritdmgUpgrade;
}
