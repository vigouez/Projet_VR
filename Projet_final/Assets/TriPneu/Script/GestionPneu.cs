﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPneu : MonoBehaviour
{
    private bool defectueux;
    private bool lisible;

    // Start is called before the first frame update
    void Start()
    {
        defectueux = (Random.Range(0.0f,1.0f) <= 0.5) ? true : false;
        lisible = (Random.Range(0.0f, 1.0f) <= 0.8) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getLisible()
    {
        return lisible;
    }

    public bool getDefectueux()
    {
        return defectueux;
    }
}
