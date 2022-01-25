using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSoundMultiple : MonoBehaviour
{
    private AudioSource my_audioSource;
    [SerializeField]
    private AudioClip my_audioClip1;
    [SerializeField]
    private AudioClip my_audioClip2;
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

        // Récupération du composant émettant le son
        my_audioSource = GetComponent<AudioSource>();

        my_audioSource.playOnAwake = isPlayedOnAwake;
        my_audioSource.loop = isLoop;
        my_audioSource.volume = my_volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySon1()
    {
        // Set du son
        my_audioSource.clip = my_audioClip1;
        // Lancement du son
        my_audioSource.Play();
    }

    public void PlaySon2()
    {
        // Set du son
        my_audioSource.clip = my_audioClip2;
        // Lancement du son
        my_audioSource.Play();
    }
}
