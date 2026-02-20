using UnityEngine;


public class TabletFollowPlayer : MonoBehaviour
{
    public Transform anchor;
    public float followSpeed = 6f;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    Rigidbody rb;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // si tenue en main → ne rien faire
        if (grab.isSelected)
            return;

        // suivre le point autour du joueur
        transform.position = Vector3.Lerp(
            transform.position,
            anchor.position,
            Time.deltaTime * followSpeed
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            anchor.rotation,
            Time.deltaTime * followSpeed
        );

        if(Vector3.Distance(transform.position, anchor.position) > 1.5f)
        {
            transform.position = anchor.position;
        }
    }
}
