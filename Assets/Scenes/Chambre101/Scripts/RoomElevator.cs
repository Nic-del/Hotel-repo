using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomElevator : MonoBehaviour
{
    public bool firstPuzzle = false;
    public bool secondPuzzle = false;
    public GameObject EnterElevatorButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        firstPuzzle = GameObject.Find("ScriptsManager").GetComponent<RoomChest>().CodeFound;
        secondPuzzle = GameObject.Find("ScriptsManager").GetComponent<Suitcase>().SuitcaseCodeFound;

        if (firstPuzzle && secondPuzzle)
        {
            EnterElevatorButton.SetActive(true);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Chambre103");
    }
}
