using UnityEngine;

public class SceneTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // vérifie que c'est le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ajout d'une note TEST");
            JournalManager.Instance.AjouterInfo("Note ajoutée depuis Scene A");
            FindObjectOfType<TabletJournal>()?.AfficherToutesInfos();
        }
    }
}
