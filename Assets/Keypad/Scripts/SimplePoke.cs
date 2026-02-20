using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using NavKeypad;

public class SimplePoke : MonoBehaviour
{
    [Header("Actions (Logique)")]
    public UnityEvent onPoke;

    [Header("Audio")]
    public AudioClip soundEffect; // Glisse le son "Bip" ici

    [Header("Réglages Animation")]
    public float btnSpeed = 0.1f;
    public float moveDist = -0.0025f; // Mettre négatif pour descendre
    public float pressTime = 0.1f;
    
    public enum ButtonAxis { AxeX_Rouge, AxeY_Vert, AxeZ_Bleu }
    public ButtonAxis axeDePoussee = ButtonAxis.AxeY_Vert; // Par défaut Y pour tes boutons TV

    // Variables internes
    private KeypadButton keypadBtn;
    private AudioSource audioSource;
    private float cooldown = 0f;
    private bool isAnimating = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
        keypadBtn = GetComponent<KeypadButton>();
        
        // On récupère ou on ajoute l'AudioSource automatiquement
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // Son en 3D
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > cooldown && !isAnimating)
            {
                // 1. Jouer le son
                if (audioSource != null && soundEffect != null)
                {
                    audioSource.PlayOneShot(soundEffect);
                }

                // 2. Logique
                if(keypadBtn != null) keypadBtn.PressButton();
                onPoke.Invoke();

                // 3. Animation
                StartCoroutine(AnimateButton());
                
                cooldown = Time.time + (btnSpeed * 2) + pressTime + 0.1f; 
            }
        }
    }

    private IEnumerator AnimateButton()
    {
        isAnimating = true;
        
        Vector3 pushDirection = Vector3.zero;
        switch (axeDePoussee)
        {
            case ButtonAxis.AxeX_Rouge: pushDirection = new Vector3(moveDist, 0, 0); break;
            case ButtonAxis.AxeY_Vert: pushDirection = new Vector3(0, moveDist, 0); break;
            case ButtonAxis.AxeZ_Bleu: pushDirection = new Vector3(0, 0, moveDist); break;
        }

        Vector3 endPos = startPos + pushDirection;

        float elapsed = 0;
        while (elapsed < btnSpeed)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / btnSpeed);
            yield return null;
        }
        transform.localPosition = endPos;

        yield return new WaitForSeconds(pressTime);

        elapsed = 0;
        while (elapsed < btnSpeed)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(endPos, startPos, elapsed / btnSpeed);
            yield return null;
        }
        transform.localPosition = startPos;

        isAnimating = false;
    }
}