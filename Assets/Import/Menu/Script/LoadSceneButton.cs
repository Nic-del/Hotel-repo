using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        Debug.Log("CLICK OK");
        SceneManager.LoadScene(sceneName);
    }
}