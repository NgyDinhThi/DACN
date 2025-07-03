using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    

    [Header("Config")]
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform[] vitrihoisinh;
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
        if (vitrihoisinh != null && vitrihoisinh.Length > 0 && playerPosition != null)
        {
            // Hồi sinh tại vị trí đầu tiên trong danh sách
            playerPosition.position = vitrihoisinh[0].position;
        }

        // Reset chỉ số
        RestartAgain();

        // Gọi animation hồi sinh
        PlayerAnimation playerAnim = playerPosition.GetComponent<PlayerAnimation>();
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
