using UnityEngine;
using System.Collections;

public class AddInfoTrigger : MonoBehaviour
{
    public SceneTrigger SceneTrigger;   

    private void OnTriggerEnter(Collider other)
    {
  
        if (other.CompareTag("Player"))
        {
            SceneTrigger.active = true;
            JournalManager.Instance.AjouterInfo("Surveillance du mort → ex-femme");
            JournalManager.Instance.AjouterInfo("Buanderie → C64");
            FindObjectOfType<TabletJournal>()?.AfficherToutesInfos();
        }
    }
}