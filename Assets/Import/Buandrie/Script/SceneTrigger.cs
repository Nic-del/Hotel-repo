using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string sceneName;
    public bool active;

    private void OnTriggerEnter(Collider other)
    {
        // vérifie que c'est le joueur
        if (other.CompareTag("Player") && active)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
