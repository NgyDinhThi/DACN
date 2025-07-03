using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    

    [Header("Config")]
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform playerTransform;

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

    public void NoButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void YesButtton()
    {
        Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn")?.transform;
        if (spawnPoint != null && playerTransform != null)
        {
            playerTransform.position = spawnPoint.position;
        }

        // Reset chỉ số
        RestartAgain();

        // Gọi animation hồi sinh
        PlayerAnimation playerAnim = playerTransform.GetComponent<PlayerAnimation>();
        if (playerAnim != null)
        {
            playerAnim.ResetPlayer();
        }

        // Reset nhiệm vụ
        QuestManager.instance.ResetAllQuests();

        // Tắt panel và tiếp tục game
        PanelDown();
        Time.timeScale = 1f;
        isDead = false;

    }    
    
    private void RestartAgain()
    {
        stats.ResetPlayer();
    }    

}
