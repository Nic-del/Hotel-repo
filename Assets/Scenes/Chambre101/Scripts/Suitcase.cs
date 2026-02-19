using TMPro;
using UnityEngine;

public class Suitcase : MonoBehaviour
{
    public TMP_InputField codeInput;
    public AudioSource suitcaseSound;
    public AudioSource wrongSound;


    public GameObject SuitcaseCanvas;
    public GameObject OpenedSuitcase;
    public GameObject ClosedSuitcase;
    public GameObject InsideSuitcase;

    public bool SuitcaseCodeFound = false;

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


        if (answer == "514")
        {
            suitcaseSound?.Play();
            SuitcaseCodeFound = true;
            SuitcaseCanvas.SetActive(false);
            OpenedSuitcase.SetActive(true);
            ClosedSuitcase.SetActive(false);
            InsideSuitcase.SetActive(true);
        }
        else
        {
            wrongSound?.Play();
            ClearField();
        }
    }

    
}
