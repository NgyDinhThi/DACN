using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PauseMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject menu;

    

    private void Start()
    {
        menu.SetActive(false);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseManager.IsPaused)
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
        PauseManager.Pause();
    }

    public void ResumeGame()
    {
        menu?.SetActive(false);
        PauseManager.Resume();
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

