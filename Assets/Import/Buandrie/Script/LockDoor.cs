using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public HingeJoint hinge;
    public bool locked = true;

    public void Unlock()
    {
        locked = false;

        JointLimits limits = hinge.limits;
        limits.min = -120;
        limits.max = 120; // angle ouverture

        hinge.limits = limits;

        Debug.Log("Porte déverrouillée");
    }
}
