using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJeux1 : MonoBehaviour
{
    private GameObject[] listePneus;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resetjeu()
    {
        EventParamBool evenementBool = new EventParamBool();
        evenementBool.Value = true;
        // On stop le serious game
        EventManager.TriggerEvent("BouttonRouge", evenementBool);

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
    }
}
