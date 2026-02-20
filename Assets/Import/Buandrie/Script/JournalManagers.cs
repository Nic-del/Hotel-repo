using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public static JournalManager Instance;

    public List<string> infos = new List<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // ✅ créer un titre (toujours en premier)
    public void CreerTitre(string titre)
    {
        if (infos.Count == 0 || !infos[0].StartsWith("#TITLE#"))
        {
            infos.Insert(0, "#TITLE#" + titre);
        }
    }

    // ✅ ajoute info + séparateur automatique
    public void AjouterInfo(string info)
    {
        if (!infos.Contains(info))
        {
            infos.Add("#SEP#"); // séparateur
            infos.Add(info);
        }
    }
}
