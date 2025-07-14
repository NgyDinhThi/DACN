using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameStateTracker.shouldResetOnNextScene = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }    

    public void QuitGame()
    {
        Debug.Log("biến mất");  
        Application.Quit();    

    }

    
}
