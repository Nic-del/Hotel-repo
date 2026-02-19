using TMPro;
using UnityEngine;

public class RoomChest : MonoBehaviour
{
    public TMP_InputField codeInput;
    public AudioSource chestSound;
    public AudioSource wrongSound;


    public GameObject ChestCanvas;
    public GameObject ChestDoor;

    public bool CodeFound = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearField()
    {
        if (codeInput != null)
            codeInput.text = string.Empty;
    }

    public void CheckAnswer()
    {
        string answer = codeInput.text.Trim();


        if (answer == "1723")
        {
            chestSound?.Play();
            CodeFound = true;
            ChestCanvas.SetActive(false);
            ChestDoor.SetActive(false);
        }
        else
        {
            wrongSound?.Play();
            ClearField();
        }
    }

    
}
