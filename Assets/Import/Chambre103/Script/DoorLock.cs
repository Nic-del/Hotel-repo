using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private HingeJoint hinge;

    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        LockDoor();
    }

    public void LockDoor()
    {
        JointLimits limits = hinge.limits;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
        hinge.useLimits = true;
    }

    public void UnlockDoor()
    {
        JointLimits limits = hinge.limits;
        limits.min = 0;
        limits.max = 180;
        hinge.limits = limits;
    }
}
