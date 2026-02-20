using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioSceneSwitch : MonoBehaviour
{
    public AudioSource audioSource;
    public string nextScene;

    void Start()
    {
        StartCoroutine(WaitForAudio());
    }

    IEnumerator WaitForAudio()
    {
        // attendre que le son commence
        while (!audioSource.isPlaying)
            yield return null;

        // attendre la fin du son
        while (audioSource.isPlaying)
            yield return null;

        // attendre 1 seconde supplémentaire
        yield return new WaitForSeconds(1f);

        // changer de scène
        SceneManager.LoadScene(nextScene);
    }
}