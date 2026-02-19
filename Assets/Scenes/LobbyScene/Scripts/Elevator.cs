using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public bool firstPuzzle = false;
    public bool secondPuzzle = false;
    public bool lastPuzzle = false;
    public GameObject EnterElevatorButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        firstPuzzle = GameObject.Find("ScriptsManager").GetComponent<Registre>().FirstPuzzleSolved;
        secondPuzzle = GameObject.Find("ScriptsManager").GetComponent<Chest>().CodeFound;
        lastPuzzle = GameObject.Find("ScriptsManager").GetComponent<Phone>().roomCalled;

        if (firstPuzzle && secondPuzzle && lastPuzzle)
        {
            EnterElevatorButton.SetActive(true);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Chambre101");
    }

}
