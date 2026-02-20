using UnityEngine;
using UnityEngine.Events; // Important pour l'événement
using UnityEngine.Video; 
using System.Collections; 
using System.Collections.Generic;

public class MaterialPuzzle : MonoBehaviour
{
    [Header("Les 7 Écrans (Element 0 = Diaporama)")]
    public List<MeshRenderer> ecransRenderers; 
    public List<VideoPlayer> videoPlayers; 

    [Header("Couleurs & Visuels")]
    public Color couleurRouge = Color.red;
    public Color couleurVerte = Color.green;
    public Color couleurBleue = Color.blue;
    public Color couleurNormale = Color.white; 
    [Range(0f, 1f)] public float intensiteCouleur = 0.5f;
    [Range(0f, 2f)] public float intensiteLumiere = 0.2f;

    [Header("Révélation")]
    public List<Texture> imagesDiaporama; 
    [Range(0.1f, 2f)] public float vitesseDiaporama = 0.8f; 
    public List<Texture> photosFixes; 

    [Header("Logique")]
    public List<int> etatsEcrans; 
    public List<int> solutionAttendue; 

    [Header("VICTOIRE (C'est ici !)")]
    public UnityEvent onPuzzleSolved; // <--- J'AI RAJOUTÉ ÇA

    [Header("Audio")]
    public AudioSource sfxSource;      
    public AudioSource ambianceSource; 
    public AudioClip staticSound;      
    public AudioClip winSound;         
    [Range(0f, 1f)] public float volumeAmbianceNormal = 0.5f; 
    [Range(0f, 1f)] public float volumeAmbianceReduit = 0.1f; 

    private bool puzzleActif = false; 
    private bool estResolu = false;

    void Start()
    {
        if(ambianceSource != null) ambianceSource.volume = volumeAmbianceNormal;
    }

    // --- ETAPES ---
    public void AllumerLesEcrans()
    {
        foreach (var vp in videoPlayers) if(vp != null) vp.Play();
        ChangerCouleurGlobale(couleurNormale);
        if (sfxSource != null && staticSound != null) { sfxSource.clip = staticSound; sfxSource.loop = true; sfxSource.Play(); }
    }

    public void ActiverPuzzle()
    {
        puzzleActif = true;
        MettreAJourVisuels();
    }

    // --- BOUTONS ---
    public void ActionBoutonBleu() { if (!puzzleActif || estResolu) return; ChangeEtat(1); ChangeEtat(2); ChangeEtat(3); ChangeEtat(4); FinDuTour(); }
    public void ActionBoutonRouge() { if (!puzzleActif || estResolu) return; ChangeEtat(5); ChangeEtat(6); FinDuTour(); }
    public void ActionBoutonVert() { if (!puzzleActif || estResolu) return; ChangeEtat(0); FinDuTour(); }
    public void ActionBoutonJaune() { if (!puzzleActif || estResolu) return; for (int i = 0; i < etatsEcrans.Count; i++) ChangeEtat(i); ChangeEtat(0); ChangeEtat(6); FinDuTour(); }

    void ChangeEtat(int index) { etatsEcrans[index]++; if (etatsEcrans[index] > 2) etatsEcrans[index] = 0; }
    void FinDuTour() { MettreAJourVisuels(); VerifierVictoire(); }

    void MettreAJourVisuels()
    {
        for (int i = 0; i < ecransRenderers.Count; i++)
        {
            Color cible = Color.white;
            if (etatsEcrans[i] == 0) cible = couleurRouge;
            else if (etatsEcrans[i] == 1) cible = couleurVerte;
            else if (etatsEcrans[i] == 2) cible = couleurBleue;

            ecransRenderers[i].material.color = Color.Lerp(Color.white, cible, intensiteCouleur);
            ecransRenderers[i].material.SetColor("_EmissionColor", cible * intensiteLumiere);
            ecransRenderers[i].material.EnableKeyword("_EMISSION");
        }
    }

    void ChangerCouleurGlobale(Color col)
    {
        foreach (var rend in ecransRenderers) { rend.material.color = col; rend.material.SetColor("_EmissionColor", col * 0.1f); rend.material.EnableKeyword("_EMISSION"); }
    }

    void VerifierVictoire()
    {
        for (int i = 0; i < etatsEcrans.Count; i++) if (etatsEcrans[i] != solutionAttendue[i]) return;

        Debug.Log("PUZZLE RÉSOLU !");
        estResolu = true;

        // DÉCLENCHER L'ÉVÉNEMENT (Pour la cinématique de fin)
        if (onPuzzleSolved != null) onPuzzleSolved.Invoke(); // <--- J'AI RAJOUTÉ ÇA

        // Audio
        if (sfxSource != null)
        {
            sfxSource.Stop(); sfxSource.loop = false;
            if (winSound) { sfxSource.PlayOneShot(winSound); if(ambianceSource != null) StartCoroutine(GererVolumeAmbiance(winSound.length)); }
        }

        // Stop Vidéos
        foreach (var vp in videoPlayers) if(vp != null) vp.Stop();

        // Affichage Photos
        for (int i = 1; i < ecransRenderers.Count; i++)
        {
            ResetMaterial(ecransRenderers[i]);
            if (i-1 < photosFixes.Count && photosFixes[i-1] != null) 
                ecransRenderers[i].material.mainTexture = photosFixes[i-1];
        }

        ResetMaterial(ecransRenderers[0]);
        StartCoroutine(JouerDiaporama());
    }

    void ResetMaterial(MeshRenderer rend)
    {
        rend.material.color = Color.white;
        rend.material.SetColor("_EmissionColor", Color.black);
        rend.material.DisableKeyword("_EMISSION");
    }

    IEnumerator JouerDiaporama()
    {
        int index = 0;
        while (true)
        {
            if (imagesDiaporama.Count > 0)
            {
                ecransRenderers[0].material.mainTexture = imagesDiaporama[index];
                index = (index + 1) % imagesDiaporama.Count;
            }
            yield return new WaitForSeconds(vitesseDiaporama);
        }
    }

    IEnumerator GererVolumeAmbiance(float dureeWinSound)
    {
        float temps = 0; float dureeFade = 1.0f;
        while (temps < dureeFade) { ambianceSource.volume = Mathf.Lerp(volumeAmbianceNormal, volumeAmbianceReduit, temps / dureeFade); temps += Time.deltaTime; yield return null; }
        ambianceSource.volume = volumeAmbianceReduit;
        yield return new WaitForSeconds(dureeWinSound);
        temps = 0; dureeFade = 2.0f;
        while (temps < dureeFade) { ambianceSource.volume = Mathf.Lerp(volumeAmbianceReduit, volumeAmbianceNormal, temps / dureeFade); temps += Time.deltaTime; yield return null; }
        ambianceSource.volume = volumeAmbianceNormal;
    }
}