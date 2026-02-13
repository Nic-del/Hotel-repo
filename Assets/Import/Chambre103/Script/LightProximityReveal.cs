using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LightProximityReveal : MonoBehaviour
{
    public List<TextMeshPro> targetTexts; // ← visible inspector
    public float fadeSpeed = 2f;

    private Dictionary<TextMeshPro, float> alphas =
        new Dictionary<TextMeshPro, float>();

    void Start()
    {
        foreach (var t in targetTexts)
        {
            alphas[t] = 0f;
            ApplyAlpha(t, 0f);
        }
    }

    void Update()
    {
        foreach (var t in targetTexts)
        {
            float target = IsTouching(t) ? 1f : 0f;

            alphas[t] = Mathf.MoveTowards(
                alphas[t],
                target,
                fadeSpeed * Time.deltaTime
            );

            ApplyAlpha(t, alphas[t]);
        }
    }

    bool IsTouching(TextMeshPro text)
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        Vector3 toText = text.transform.position - origin;

        float maxDistance = 70f;   // longueur du faisceau
        float coneAngle = 70f;    // ouverture du faisceau

        // 1️⃣ trop loin
        if (toText.magnitude > maxDistance)
            return false;

        // 2️⃣ derrière la lampe
        if (Vector3.Dot(forward, toText.normalized) <= 0f)
            return false;

        // 3️⃣ angle du cône
        float angle = Vector3.Angle(forward, toText);

        if (angle < coneAngle)
            return true;

        return false;
    }



    void ApplyAlpha(TextMeshPro text, float a)
    {
        Color c = text.color;
        c.a = a;
        text.color = c;
    }
}
