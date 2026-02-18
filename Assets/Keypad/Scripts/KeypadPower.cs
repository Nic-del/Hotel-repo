using UnityEngine;
using TMPro;
using NavKeypad; // On utilise le namespace de ton asset

public class KeypadPower : MonoBehaviour
{
    [Header("Références")]
    public Keypad keypadScript;      // Le script principal du digicode
    public Renderer screenRenderer;  // L'écran (pour l'éteindre)
    public TMP_Text screenText;      // Le texte (0123...)
    public GameObject buttonsParent; // Le parent qui contient tous les boutons

    [Header("Materials")]
    public Material screenOffMat;    // Optionnel : un matériau noir (ou on change juste la couleur)

    void Start()
    {
        // Au démarrage : ON COUPE TOUT
        TurnOff();
    }

    public void TurnOff()
    {
        // 1. Désactiver le script logique
        if(keypadScript != null) keypadScript.enabled = false;

        // 2. Éteindre l'écran (On met l'émission à 0 pour faire noir)
        if (screenRenderer != null)
        {
            screenRenderer.material.DisableKeyword("_EMISSION");
            screenRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            screenRenderer.material.SetColor("_EmissionColor", Color.black);
        }

        // 3. Cacher le texte
        if (screenText != null) screenText.gameObject.SetActive(false);

        // 4. Désactiver les boutons (Pour qu'on ne puisse pas cliquer)
        // On cherche tous les colliders des boutons et on les désactive
        if (buttonsParent != null)
        {
            Collider[] buttonColliders = buttonsParent.GetComponentsInChildren<Collider>();
            foreach (Collider col in buttonColliders)
            {
                col.enabled = false;
            }
        }
    }

    public void TurnOn()
    {
        // 1. Réactiver le script
        if(keypadScript != null) keypadScript.enabled = true;

        // 2. Rallumer l'écran
        if (screenRenderer != null)
        {
            screenRenderer.material.EnableKeyword("_EMISSION");
            screenRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            
            // CORRECTION : On utilise la couleur actuelle du matériau (Ton Bleu) au lieu de forcer l'orange
            // On multiplie par 2 pour que ça brille bien fort
            Color maCouleurBleue = screenRenderer.material.GetColor("_EmissionColor");
            // Si c'est noir, on met du blanc par défaut pour éviter le bug
            if(maCouleurBleue.maxColorComponent <= 0.1f) maCouleurBleue = Color.cyan; 
            
            screenRenderer.material.SetColor("_EmissionColor", maCouleurBleue * 2f); 
        }

        // 3. Afficher le texte
        if (screenText != null) screenText.gameObject.SetActive(true);

        // 4. Réactiver les boutons
        if (buttonsParent != null)
        {
            Collider[] buttonColliders = buttonsParent.GetComponentsInChildren<Collider>();
            foreach (Collider col in buttonColliders)
            {
                col.enabled = true;
            }
        }
    }
}