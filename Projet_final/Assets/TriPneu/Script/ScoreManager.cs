using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int NombrePneus;
    private GameObject[] listePneus;

    // Start is called before the first frame update
    void Start()
    {
        NombrePneus = 0;
        EventManager.StartListening("Comptage", UpdateSommePneus);
        EventManager.StartListening("ResetNombrePneus", ResetNombrePneus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSommePneus(EventParam evenement)
    {
        EventComptagePneus evenementComptagePneus = (EventComptagePneus)evenement;
        NombrePneus += evenementComptagePneus.Value;
        Debug.Log("NombrePneus : " + NombrePneus);
        if (NombrePneus >= 10)
        {
            listePneus = GameObject.FindGameObjectsWithTag("Pneu");
            foreach (GameObject pneu in listePneus)
            {
                Destroy(pneu);
                EventParamBool resetJeu = new EventParamBool();
                resetJeu.Value = true;
                EventManager.TriggerEvent("ResetNombrePneus", resetJeu);
            }
            EventManager.TriggerEvent("stopPneus", new EventParam());
        }
    }

    void ResetNombrePneus(EventParam evenement)
    {
        NombrePneus = 0;
    }
}
