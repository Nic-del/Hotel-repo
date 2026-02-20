using UnityEngine;
using TMPro;

public class LightProximityReveal : MonoBehaviour
{
    public TextMeshPro targetText;
    public float fadeSpeed = 2f;

    private float currentAlpha = 0f;
    private bool isTouching = false;

    void Start()
    {
        SetAlpha(0f); // invisible au départ
    }

    void Update()
    {
        float targetAlpha = isTouching ? 1f : 0f;

        currentAlpha = Mathf.MoveTowards(
            currentAlpha,
            targetAlpha,
            fadeSpeed * Time.deltaTime
        );

        SetAlpha(currentAlpha);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Text")) return;

        Debug.Log("Ajout d'une note TEST");
        JournalManager.Instance.AjouterInfo("Code chambre 103 : 7 3 12 1");
        FindObjectOfType<TabletJournal>()?.AfficherToutesInfos();
        isTouching = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Text")) return;

        Debug.Log("EXIT : " + other.name);
        isTouching = false;
    }


    void SetAlpha(float a)
    {
        Color c = targetText.color;
        c.a = Mathf.Clamp01(a);
        targetText.color = c;
    }
}