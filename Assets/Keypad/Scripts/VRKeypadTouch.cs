using UnityEngine;
using NavKeypad; // On utilise le namespace de ton asset

public class VRKeypadTouch : MonoBehaviour
{
    private KeypadButton keypadBtn;

    void Start()
    {
        keypadBtn = GetComponent<KeypadButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est bien la main du joueur qui touche (Tag "Player" ou "Hand")
        // Assure-toi que tes mains VR ont un SphereCollider en mode Trigger !
        if (other.CompareTag("Player") || other.CompareTag("Hand")) 
        {
            if(keypadBtn != null)
            {
                keypadBtn.PressButton(); // Simule l'appui
            }
        }
    }
}