using UnityEngine;
using NavKeypad; // On utilise le namespace de ton asset

public class SimplePoke : MonoBehaviour
{
    private KeypadButton btnScript;
    private float cooldown = 0f; // Pour éviter de cliquer 10 fois en 1 seconde

    void Start()
    {
        // On récupère le script du bouton automatiquement
        btnScript = GetComponent<KeypadButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si l'objet qui touche a le tag "Player" (Ton doigt)
        if (other.CompareTag("Player"))
        {
            // On vérifie le cooldown
            if (Time.time > cooldown)
            {
                // CORRECTION ICI : On utilise "value" (minuscule) si elle est publique,
                // mais elle est "private" dans le script d'origine.
                // On va donc afficher juste un message générique si on ne peut pas lire la valeur.
                Debug.Log("DOIGT DÉTECTÉ SUR LE BOUTON !");
                
                // On appuie sur le bouton
                if(btnScript != null) 
                {
                    btnScript.PressButton();
                }
                
                // On attend 0.3 secondes avant de pouvoir recliquer
                cooldown = Time.time + 0.3f; 
            }
        }
    }
}