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

    // On met à jour le nombre de pneu bien catégorisé dans les bonnes bennes
    void UpdateSommePneus(EventParam evenement)
    {
        EventComptagePneus evenementComptagePneus = (EventComptagePneus)evenement;
        NombrePneus += evenementComptagePneus.Value;
        
        // Quand le nombre de pneu atteind 10, on fini le serious game
        if (NombrePneus >= 10)
        {
            listePneus = GameObject.FindGameObjectsWithTag("Pneu");
            foreach (GameObject pneu in listePneus)
            {
                // Destruction de chaque pneu instanciés
                Destroy(pneu);

                EventParamBool resetJeu = new EventParamBool();
                resetJeu.Value = true;
                // On remet à 0 le nombre de pneus correctement catégorisés
                EventManager.TriggerEvent("ResetNombrePneus", resetJeu);
            }
            
            // On arrête le chrono du serious game
            EventManager.TriggerEvent("stopPneus", new EventParam());
        }
    }

    void ResetNombrePneus(EventParam evenement)
    {
        NombrePneus = 0;
    }
}
