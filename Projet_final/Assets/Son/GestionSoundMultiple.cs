using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSoundMultiple : MonoBehaviour
{
    private AudioSource my_audioSource;
    [SerializeField]
    private AudioClip[] my_audioClips;
    [SerializeField]
    private bool isPlayedOnAwake;
    [SerializeField]
    private bool isLoop;
    [SerializeField]
    [Range(0, 1)]
    private float my_volume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ajout d'un composant audio à l'objet possédant le script
        gameObject.AddComponent<AudioSource>();

        // Récupération du composant de l'objet émettant le son
        my_audioSource = GetComponent<AudioSource>();

        // Si l'AudioSource existe
        if (my_audioSource != null)
        {
            // Paramétrage de l'AudioSource
            my_audioSource.playOnAwake = isPlayedOnAwake;
            my_audioSource.loop = isLoop;
            my_audioSource.volume = my_volume;
        }
    }

    public void PlaySon(int n)
    {
        // Si l'AudioSource et le son passé en paramètre existent
        if (my_audioSource != null && n< my_audioClips.Length && my_audioClips[n] != null)
        {
            // Set du son
            my_audioSource.clip = my_audioClips[n];
            // Lancement du son
            my_audioSource.Play();
        }
    }

    public void PlaySon1()
    {
        PlaySon(0);
    }

    public void PlaySon2()
    {
        PlaySon(1);
    }


    public void StopSon()
    {
        if (my_audioSource != null)
        {
            // Arrêt du son
            my_audioSource.Stop();
        }
    }
}
