using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnGrabExec : MonoBehaviour
{

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    public SceneTrigger trigger;
    bool alreadyGrabbed = false;

    
    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (alreadyGrabbed) return;
        Debug.Log("Objet attrapé !");   
        alreadyGrabbed = true;
        
        // Exemple : ajouter une note
        JournalManager.Instance.AjouterInfo("Maria & Alex : liaison cachée");
        JournalManager.Instance.AjouterInfo("Maria exploitée sexuellement");
        trigger.active = true;
        FindObjectOfType<TabletJournal>()?.AfficherToutesInfos();
    }
}
