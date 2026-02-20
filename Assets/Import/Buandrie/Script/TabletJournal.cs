using System.Collections;
using UnityEngine;
using TMPro;

public class TabletJournal : MonoBehaviour
{
    public Transform contentParent;
    public GameObject infoCardPrefab;

    public GameObject separatorPrefab;
    public GameObject titlePrefab;

    IEnumerator Start()
{
    yield return null; // attendre 1 frame

    JournalManager.Instance.CreerTitre("Block note");
    
    //JournalManager.Instance.AjouterInfo("Code 7F");
    AfficherToutesInfos();
}

    public void AfficherToutesInfos()
{
    foreach (Transform child in contentParent)
        Destroy(child.gameObject);

    foreach (string info in JournalManager.Instance.infos)
    {
        GameObject prefab = infoCardPrefab;
        string text = info;

        // ---------- TITRE ----------
        if (info.StartsWith("#TITLE#"))
        {
            prefab = titlePrefab;
            text = info.Replace("#TITLE#", "");
        }
        // ---------- SEPARATEUR ----------
        else if (info == "#SEP#")
        {
            Instantiate(separatorPrefab, contentParent);
            continue;
        }

        GameObject card = Instantiate(prefab, contentParent);
        card.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
}
