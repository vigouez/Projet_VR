﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionAnim : MonoBehaviour
{
    public Animator anim;
    bool deja = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {

    }

    public void startAnim()
    {
        if (!deja) {
            anim.Play("Anim");
            deja = true;
        }
        anim.speed = 1;
    }

    public void stopAnim()
    {
        anim.speed = 0;
    }
}
