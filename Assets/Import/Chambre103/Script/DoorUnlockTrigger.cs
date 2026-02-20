using UnityEngine;

public class DoorUnlockTrigger : MonoBehaviour
{
    public HingeJoint doorHinge;
    public string requiredTag = "Key";

    bool unlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag(requiredTag))
        {
            UnlockDoor();
        }
    }

    void UnlockDoor()
    {
        unlocked = true;

        JointLimits limits = doorHinge.limits;

        // ouvre complètement la porte
        limits.min = -112.7f;
        limits.max = 114.4f;

        doorHinge.limits = limits;

        Debug.Log("Porte déverrouillée !");
    }
}
