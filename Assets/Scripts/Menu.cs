using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private string level1SceneName;
    [SerializeField] private string level2SceneName;
    [SerializeField] private string menuStartSceneName;

    public void StartGame()
    {
        panel.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1SceneName);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2SceneName);
    }

    public void ReturnToMenu()
    {
        if (SceneManager.GetActiveScene().name == menuStartSceneName)
        {
            panel.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(menuStartSceneName);
        }
    }
}
