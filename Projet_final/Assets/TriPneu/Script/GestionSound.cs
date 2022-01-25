using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSound : MonoBehaviour
{
    private AudioSource my_audioSource;
    [SerializeField]
    private AudioClip my_audioClip;
    [SerializeField]
    private bool isPlayedOnAwake;
    [SerializeField]
    private bool isLoop;
    [SerializeField]
    [Range(0,1)]
    private float my_volume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ajout d'un composant audio à l'objet possédant le script
        gameObject.AddComponent<AudioSource>();
        
        // Récupération du composant émettant le son
        my_audioSource = GetComponent<AudioSource>();

        if(my_audioSource != null && my_audioClip != null)
        {
            // Set du son
            my_audioSource.clip = my_audioClip;

            my_audioSource.playOnAwake = isPlayedOnAwake;
            my_audioSource.loop = isLoop;
            my_audioSource.volume = my_volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySon()
    {
        if (my_audioSource != null && my_audioClip != null)
        {
            // Lancement du son
            my_audioSource.Play();
        }
    }

    public void StopSon()
    {
        if (my_audioSource != null && my_audioClip != null)
        {
            // Arrêt du son
            my_audioSource.Stop();
        }
    }
}
