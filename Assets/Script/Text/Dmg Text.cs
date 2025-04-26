using TMPro;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI dmgtext;

    public void textsatthuong(float dmg)
    {
        dmgtext.text = dmg.ToString();
       

    }

    public void huyText()
    {
        Destroy(gameObject);
    
    }
}
