using TMPro;
using UnityEngine;

public class SimpleTMPLamp : MonoBehaviour
{
    public TextMeshPro text;
    public float revealDistance = 1.5f;

    void Update()
    {
        float d = Vector3.Distance(transform.position, text.transform.position);
        text.fontMaterial.SetFloat("_FaceDilate", d < revealDistance ? 0.2f : -1f);
    }
}
