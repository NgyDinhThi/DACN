using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PauseMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject menu;

    public static bool isPause;

    private void Start()
    {
        menu.SetActive(false);
        isPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Dừng game và hiển thị menu tạm dừng
    public void PauseGame()
    {
        menu?.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void ResumeGame()
    {
        menu?.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void GotoMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("biến mất");
        Application.Quit();

    }
}

