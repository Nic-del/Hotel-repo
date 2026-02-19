using TMPro;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public TMP_InputField phoneInput;
    public AudioSource busyLineSound;
    public AudioSource callingSound;
    public AudioSource wrongSound;


    public GameObject TextAutre;
    public GameObject TextAnswer;

    public bool roomCalled = false;

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
        if (phoneInput != null)
            phoneInput.text = string.Empty;
    }

    public void CheckAnswer()
    {
        string answer = phoneInput.text.Trim();


        if (answer == "#103*")
        {
            callingSound?.Play();
            TextAutre.SetActive(false);
            TextAnswer.SetActive(true);
            ClearField();
            roomCalled = true;
        }
        else
        {
            if (answer == "#101*")
            {
                busyLineSound?.Play();
                TextAutre.SetActive(true);
                TextAnswer.SetActive(false);
                ClearField();
            }
            else
            {
                wrongSound?.Play();
                ClearField();
            }
                
            
        }
    }

}
