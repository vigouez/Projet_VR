using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionAffichageEcran : MonoBehaviour
{

    MeshRenderer affichage;
    [SerializeField]
    Material matError;
    [SerializeField]
    Material matWaiting;
    [SerializeField]
    Material matNoScan;
    [SerializeField]
    Material matConform;
    EventParamBool evenementLumiere;

    // Start is called before the first frame update
    void Start()
    {
        affichage = gameObject.GetComponent<MeshRenderer>();
        affichage.material = matWaiting;
        
        EventManager.StartListening("AffichageEcran", UpdateScreen);

        evenementLumiere = new EventParamBool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScreen(EventParam evenement)
    {
        EventAffichageEcran evenementString = (EventAffichageEcran)evenement;

        switch (evenementString.Value)
        {
            case "Error":
                affichage.material = matError;
                evenementLumiere.Value = true;
                EventManager.TriggerEvent("RedLight", evenementLumiere);
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("GreenLight", evenementLumiere);
                break;
            case "NoScan":
                affichage.material = matNoScan;
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("RedLight", evenementLumiere);
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("GreenLight", evenementLumiere);
                break;
            case "Conform":
                affichage.material = matConform;
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("RedLight", evenementLumiere);
                evenementLumiere.Value = true;
                EventManager.TriggerEvent("GreenLight", evenementLumiere);
                break;
            default:
                affichage.material = matWaiting;
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("RedLight", evenementLumiere);
                evenementLumiere.Value = false;
                EventManager.TriggerEvent("GreenLight", evenementLumiere);
                break;
        }
    }
}
