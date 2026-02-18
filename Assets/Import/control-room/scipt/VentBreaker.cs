using UnityEngine;

public class VentBreaker : MonoBehaviour
{
    [Header("Réglages")]
    public float forceEjection = 3f; // La puissance du saut
    public AudioClip sonMetal;       // Le bruit "CLANG"

    private Rigidbody rb;
    private bool estCassee = false;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Au début, la grille est soudée au sol
        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si c'est déjà cassé, on arrête
        if (estCassee) return;

        // Si l'objet qui touche a le tag "Crowbar" (Pied-de-biche)
        if (other.CompareTag("Crowbar"))
        {
            CasserLaGrille();
        }
    }

    void CasserLaGrille()
    {
        estCassee = true;

        if (rb != null)
        {
            // 1. On libère la physique
            rb.isKinematic = false;
            rb.useGravity = true;

            // 2. On la fait sauter vers le haut et un peu sur le côté
            // On ajoute une rotation pour que ça fasse réaliste
            rb.AddForce(Vector3.up * forceEjection + Vector3.right * 0.5f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        }

        // 3. Jouer le son
        if (audioSource != null && sonMetal != null)
        {
            audioSource.PlayOneShot(sonMetal);
        }

        Debug.Log("GRILLE CASSÉE !");
    }
}