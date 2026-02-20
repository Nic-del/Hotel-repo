using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string firstScene;

    public void StartGame()
    {
        SceneManager.LoadScene(firstScene);
    }
}