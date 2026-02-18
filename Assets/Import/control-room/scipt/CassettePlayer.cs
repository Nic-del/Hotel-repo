using UnityEngine;
using UnityEngine.Events;

public class CassettePlayer : MonoBehaviour
{
    [Header("Réglages")]
    public GameObject cassetteInReader; // La cassette B (déjà placée, cachée au début)
    public AudioSource audioSource;     // Pour le son "Clac" d'insertion
    public AudioClip insertSound;       // Le son
    
    [Header("Événement")]
    public UnityEvent onCassetteInserted; // Ce qui se passe après (allumer l'écran, lancer la vidéo...)

    private bool hasCassette = false;

    void Start()
    {
        // Au début, on cache la cassette du lecteur
        if (cassetteInReader != null) 
            cassetteInReader.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si on a déjà une cassette, on ne fait rien
        if (hasCassette) return;

        // Si l'objet qui touche a le tag "Cassette" (Important !)
        if (other.CompareTag("Cassette"))
        {
            InsertCassette(other.gameObject);
        }
    }

    void InsertCassette(GameObject cassetteInHand)
    {
        hasCassette = true;

        // 1. Détruire la cassette que le joueur tient
        Destroy(cassetteInHand);

        // 2. Afficher la cassette bien placée
        if (cassetteInReader != null)
            cassetteInReader.SetActive(true);

        // 3. Jouer le son
        if (audioSource != null && insertSound != null)
            audioSource.PlayOneShot(insertSound);

        // 4. Déclencher l'événement (Lancer la vidéo...)
        Debug.Log("CASSETTE INSÉRÉE !");
        onCassetteInserted.Invoke();
    }
}