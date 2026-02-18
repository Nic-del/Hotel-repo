using UnityEngine;

public class DrawerLock : MonoBehaviour
{
    private ConfigurableJoint joint;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        
        // AU DÉBUT : ON BLOQUE TOUT
        if (joint != null)
        {
            // On met l'axe X (le tirage) sur LOCKED
            var motion = joint.xMotion;
            motion = ConfigurableJointMotion.Locked;
            joint.xMotion = motion;
        }
    }

    // Appelé par le Digicode
    public void UnlockDrawer()
    {
        if (joint != null)
        {
            // DÉVERROUILLAGE : On met l'axe X sur LIMITED
            var motion = joint.xMotion;
            motion = ConfigurableJointMotion.Limited;
            joint.xMotion = motion;
            
            Debug.Log("Tiroir DÉVERROUILLÉ !");
        }
    }
}