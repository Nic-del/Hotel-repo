using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EndGameSimple : MonoBehaviour
{
    [Header("Configuration")]
    public Transform playerRoot;        
    public Transform pointTeleportation; 
    public CanvasGroup ecranNoirFade;   
    
    [Header("Éléments à Afficher")]
    public GameObject policierNPC;      
    public GameObject dialogueUI;       
    
    [Header("Navigation")]
    public string nomSceneSuivante;     

    [Header("Blocage du Joueur")]
    public List<MonoBehaviour> scriptsMouvement; 

    [Header("Audio (Nouveau)")]
    public AudioSource audioSource;      // <--- NOUVEAU
    public AudioClip sonTeleportation;   // <--- NOUVEAU (Le bruit de Woosh/Glitch)

    // --- 1. DÉCLENCHÉ PAR LE PUZZLE TV ---
    public void LancerSequence()
    {
        StartCoroutine(SequenceChrono());
    }

    IEnumerator SequenceChrono()
    {
        // A. On attend 10 secondes
        yield return new WaitForSeconds(10f);

        // B. On coupe les mouvements
        foreach (var script in scriptsMouvement)
        {
            if (script != null) script.enabled = false;
        }

        // C. Fade au Noir
        yield return StartCoroutine(FaireFade(0, 1, 1f));

        // D. TÉLÉPORTATION + SON
        playerRoot.position = pointTeleportation.position;
        
        Vector3 rot = pointTeleportation.rotation.eulerAngles;
        playerRoot.rotation = Quaternion.Euler(0, rot.y, 0);

        // --- JOUER LE SON ICI ---
        if (audioSource != null && sonTeleportation != null)
        {
            audioSource.PlayOneShot(sonTeleportation);
        }

        // On active le policier
        if(policierNPC != null) policierNPC.SetActive(true);
        if(dialogueUI != null) dialogueUI.SetActive(true);

        // Petite pause dans le noir pour entendre le son
        yield return new WaitForSeconds(0.5f);

        // E. Fade In
        yield return StartCoroutine(FaireFade(1, 0, 1f));
    }

    // --- 2. DÉCLENCHÉ PAR LE BOUTON DU DIALOGUE ---
    public void BoutonSuivantAppuye()
    {
        StartCoroutine(ChargerScene());
    }

    IEnumerator ChargerScene()
    {
        yield return StartCoroutine(FaireFade(0, 1, 1f));
        SceneManager.LoadScene(nomSceneSuivante);
    }

    IEnumerator FaireFade(float debut, float fin, float duree)
    {
        float t = 0;
        while (t < duree)
        {
            if (ecranNoirFade != null) 
                ecranNoirFade.alpha = Mathf.Lerp(debut, fin, t / duree);
            
            t += Time.deltaTime;
            yield return null;
        }
        if (ecranNoirFade != null) ecranNoirFade.alpha = fin;
    }
}