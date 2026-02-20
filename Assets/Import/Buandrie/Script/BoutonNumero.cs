using UnityEngine;

public class BoutonDigicode : MonoBehaviour
{
    public string symbole; // "A", "B", "7", etc
    public DistributeurCode distributeur;

    void OnMouseDown()
    {
        distributeur.AjouterSymbole(symbole);
    }
}
