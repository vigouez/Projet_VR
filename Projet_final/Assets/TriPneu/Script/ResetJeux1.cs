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
        EventManager.TriggerEvent("BouttonRouge", evenementBool);

        listePneus = GameObject.FindGameObjectsWithTag("Pneu");
        foreach (GameObject pneu in listePneus)
        {
            Destroy(pneu);
            EventParamBool resetJeu = new EventParamBool();
            resetJeu.Value = true;
            EventManager.TriggerEvent("ResetNombrePneus", resetJeu);
        }
    }
}
