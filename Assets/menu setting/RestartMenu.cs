using UnityEngine;

public class RestartMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerStats stats;

    public static bool isDead;

    private void Start()
    {
        menu.SetActive(false);
        isDead = false;
    }

    private void Update()
    {
        if (stats.health <= 0f)
        {
            PanelUp();
        }
        if (stats.health > 0f)
        {
            PanelDown();
        }
    
    }

    public void PanelUp()
    {
        menu?.SetActive(true);
        Time.timeScale = 1f;
        isDead = true;
    }

    public void PanelDown()
    {
        menu?.SetActive(false);
        Time.timeScale = 1f;
        isDead = false;
    }


}
