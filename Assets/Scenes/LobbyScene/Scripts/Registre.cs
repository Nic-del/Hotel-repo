using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Registre : MonoBehaviour
{

    public bool FirstPuzzleSolved = false;

    public GameObject Registre_part1;
    public GameObject Registre_part2;
    public GameObject Registre_part3;

    public GameObject otherIcons;

    public TMP_InputField roomInput;
    public AudioSource correctSound;
    public AudioSource wrongSound;

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
        if (roomInput != null)
            roomInput.text = string.Empty;
    }

    public void CheckAnswer()
    {
        string answer = roomInput.text.Trim();
        

        if (answer == "101")
        {
            correctSound?.Play();
            FirstPuzzleSolved=true;
            Registre_part2.SetActive(false);
            Registre_part3.SetActive(true);
            otherIcons.SetActive(true);
        }
        else
        {
            wrongSound?.Play();
            ClearField();
        }
    }

    public void touchIconClicked()
    {
        if (!FirstPuzzleSolved) 
        { 
            Registre_part1.SetActive(true);
        }
        else
        {
            Registre_part3.SetActive(true);
        }
    }

}
