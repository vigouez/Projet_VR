using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPneu : MonoBehaviour
{
    private bool defectueux;
    private bool lisible;

    // Start is called before the first frame update
    void Start()
    {
        // Chaque pneu a : 50% de chance d'être défectueux et 80% de chance d'être lisible par détection automatique
        defectueux = (Random.Range(0.0f,1.0f) <= 0.5) ? true : false;
        lisible = (Random.Range(0.0f, 1.0f) <= 0.8) ? true : false;
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
