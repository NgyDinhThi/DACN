using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    [Header("Tên class")]
    public string sceneToLoad = "GameScene";

    public void OnClickLoad()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UsingSaveSystem usingSaveSystem = FindFirstObjectByType<UsingSaveSystem>();

        if (usingSaveSystem != null)
        {
            usingSaveSystem.UseLoadPlayer();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
