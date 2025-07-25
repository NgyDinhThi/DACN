using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private const string IntroSeenKey = "HasSeenIntro";
  
    [Header("Scene Name Config")]
    [SerializeField] private string introSceneName ;   // Tên scene intro
    [SerializeField] private string gameSceneName ;     // Tên scene chính

    public void PlayGame()
    {
        GameStateTracker.shouldResetOnNextScene = true;

        // Nếu chưa xem intro thì vào scene intro trước
        if (!PlayerPrefs.HasKey(IntroSeenKey))
        {
            PlayerPrefs.SetInt(IntroSeenKey, 1); // Đánh dấu đã xem
            PlayerPrefs.Save();
            SceneManager.LoadScene(introSceneName);
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    public void LoadOtherScene()
    {
        GameStateTracker.shouldResetOnNextScene = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void QuitGame()
    {
        Debug.Log("biến mất");  
        Application.Quit();    

    }

    
}
