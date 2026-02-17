using UnityEngine;
using UnityEngine.Events;

public class ElectricalBoxPuzzle : MonoBehaviour
{
    [Header("--- ÉLÉMENTS DU BOITIER ---")]
    public GameObject door;             
    public GameObject fuseInBox;        
    public HingeJoint leverJoint;       // LE NOUVEAU TRUC : Le composant HingeJoint du levier
    public Renderer statusLight;        
    
    [Header("--- RÉGLAGES LEVIER ---")]
    public float angleToTrigger = 45f; // A quel angle le courant s'active (ex: 45 degrés)

    [Header("--- MATÉRIAUX ---")]
    public Material redLightMat;        
    public Material greenLightMat;      

    [Header("--- AUDIO ---")]
    public AudioSource audioSource;
    public AudioClip breakSound;        
    public AudioClip fuseSnapSound;     
    public AudioClip electricHumSound;  

    [Header("--- ÉVÉNEMENTS ---")]
    public UnityEvent onPowerRestored;  

    // États internes
    private bool isDoorOpen = false;
    private bool hasFuse = false;
    private bool isPowerOn = false;

    void Start()
    {
        // Initialisation
        if (door != null)
        {
            door.SetActive(true);
            Rigidbody rb = door.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true; 
        }

        fuseInBox.SetActive(false); 
        
        if(statusLight != null)
            statusLight.material = redLightMat;
    }

    // Vérifie l'angle du levier à chaque image
    void Update()
    {
        // Si le courant est déjà mis ou si la porte est fermée, on ne fait rien
        if (isPowerOn || !isDoorOpen) return;

        // Si on a le levier connecté
        if (leverJoint != null)
        {
            // On récupère l'angle actuel du levier (grâce au Hinge Joint)
            float currentAngle = leverJoint.angle;

            // Si l'angle dépasse la limite (ex: on a baissé le levier) ET qu'on a le fusible
            if (currentAngle >= angleToTrigger && hasFuse)
            {
                ActivatePower();
            }
            // Feedback sonore optionnel : CLIC quand on essaie sans fusible
            else if (currentAngle >= angleToTrigger && !hasFuse)
            {
                // Ici tu pourrais mettre un son de "clic vide" si tu veux
            }
        }
    }

    void ActivatePower()
    {
        isPowerOn = true;
        statusLight.material = greenLightMat;
        audioSource.PlayOneShot(electricHumSound);
        onPowerRestored.Invoke();
        Debug.Log("COURANT RÉTABLI !");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDoorOpen && other.CompareTag("Crowbar"))
        {
            BreakDoor();
        }

        if (isDoorOpen && !hasFuse && other.CompareTag("Fuse"))
        {
            PlaceFuse(other.gameObject);
        }
    }

    void BreakDoor()
    {
        isDoorOpen = true;
        Rigidbody doorRb = door.GetComponent<Rigidbody>();
        if (doorRb != null)
        {
            doorRb.isKinematic = false;
            doorRb.useGravity = true;
            door.transform.parent = null; 
            doorRb.AddForce(transform.forward * 1.5f + transform.up * 0.5f, ForceMode.Impulse); 
        }
        audioSource.PlayOneShot(breakSound);
    }

    void PlaceFuse(GameObject fuseInHand)
    {
        hasFuse = true;
        Destroy(fuseInHand); 
        fuseInBox.SetActive(true);
        audioSource.PlayOneShot(fuseSnapSound);
    }
}