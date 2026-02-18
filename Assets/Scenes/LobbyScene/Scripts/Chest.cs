using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public TMP_InputField codeInput;
    public AudioSource chestSound;
    public AudioSource wrongSound;


    public GameObject ChestCanvas1;
    public GameObject ChestCanvas2;

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


        if (answer == "6247")
        {
            chestSound?.Play();
            CodeFound = true;
            ChestCanvas1.SetActive(false);
            ChestCanvas2.SetActive(true);
        }
        else
        {
            wrongSound?.Play();
            ClearField();
        }
    }

    public void touchIconClicked()
    {
        if (!CodeFound)
        {
            ChestCanvas1.SetActive(true);
        }
        else
        {
            ChestCanvas2.SetActive(true);
        }
    }


}
