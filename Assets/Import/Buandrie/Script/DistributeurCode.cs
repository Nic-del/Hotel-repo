using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;


public class DistributeurCode : MonoBehaviour
{
    public List<string> codeCorrect = new List<string>() { "A", "7", "F" };
    private List<string> codeEntre = new List<string>();

    public bool cartePresente = false;

    public TextMeshPro texteEcran;

    public LockDoor porte;


    void Update()
    {
        
    }

    void AfficherMessage(string message, Color couleur)
    {
        texteEcran.text = message;
        texteEcran.color = couleur;
    }

    void AfficherCodeEntre()
    {
        string texte = "";

        foreach (string s in codeEntre)
        {
            texte += s;
        }

        texteEcran.text = texte;
        texteEcran.color = Color.white;
    }


    public void AjouterSymbole(string symbole)
    {
        if (!cartePresente)
        {
            AfficherMessage("CARTE REQUISE", Color.red);
            return;
        }

        codeEntre.Add(symbole);
        Debug.Log("Ajout : " + symbole);
        AfficherCodeEntre();
        VerifierCode();
    }

    void VerifierCode()
    {
        if (codeEntre.Count != codeCorrect.Count)
            return;

        for (int i = 0; i < codeCorrect.Count; i++)
        {
            if (codeEntre[i] != codeCorrect[i])
            {
                AfficherMessage("CODE INCORRECT", Color.red);
                StartCoroutine(ResetEcran());
                ResetCode();
                return;
            }
        }

        AfficherMessage("ACCES AUTORISE", Color.green);
        StartCoroutine(ResetEcran());
        porte.Unlock();
        ResetCode();
    }

    void ResetCode()
    {
        codeEntre.Clear();
    }

    IEnumerator ResetEcran()
    {
        yield return new WaitForSeconds(2f);
        AfficherMessage("ENTER CODE", Color.white);
    }

}
