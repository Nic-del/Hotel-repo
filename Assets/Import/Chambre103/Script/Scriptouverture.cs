using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Scriptouverture : MonoBehaviour
{
    public HingeJoint doorHinge;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    
    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        doorHinge.useLimits = true;
        doorHinge.limits = new JointLimits { min = -90, max = 90 };
    }

    void OnRelease(SelectExitEventArgs args)
    {
        doorHinge.limits = new JointLimits { min = 0, max = 0 };
    }
}
