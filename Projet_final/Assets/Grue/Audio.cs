using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource m_MyAudioSource;
    bool lastb = false;
    public bool b = false;
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (b && lastb != b)
        {
            m_MyAudioSource.Play();
        }
        if (!b && lastb != b)
        {
            m_MyAudioSource.Stop();
        }
        lastb = b;
    }
}
