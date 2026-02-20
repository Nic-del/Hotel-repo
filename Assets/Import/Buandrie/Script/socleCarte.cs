using UnityEngine;
using System.Collections;
using TMPro;


public class SocleCarte : MonoBehaviour
{
    public Transform pointInsertion;
    public DistributeurCode distributeur;
    public TextMeshPro texteEcran;

    void AfficherMessage(string message, Color couleur)
    {
        texteEcran.text = message;
        texteEcran.color = couleur;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Carte"))
            return;

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab =
            other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        Rigidbody rb = other.GetComponent<Rigidbody>();

        // 1️⃣ FORCE RELEASE si tenue en main
        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.SelectExit(
                grab.firstInteractorSelecting,
                grab
            );
        }

        // 2️⃣ Désactive le grab
        grab.enabled = false;

        // 3️⃣ Stop physique
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // 4️⃣ SNAP parfait
        other.transform.SetParent(pointInsertion);
        other.transform.localPosition = Vector3.zero;
        other.transform.localRotation = Quaternion.identity;

        distributeur.cartePresente = true;

        AfficherMessage("CARTE VALIDE", Color.green);
        StartCoroutine(ResetEcran());
    }

    IEnumerator ResetEcran()
    {
        yield return new WaitForSeconds(2f);
        AfficherMessage("ENTER CODE", Color.white);
    }
}
