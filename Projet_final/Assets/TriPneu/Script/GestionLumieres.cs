using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionLumieres : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Quelque soit le nom de l'objet appelant l'event, on lancera la méthode 'SwitchLumiere'
        // Permet de facilement ajouter une nouvelle lumière, sans avoir à mettre en place une méthode pour chacune
        EventManager.StartListening(gameObject.name, SwitchLumiere);
    }

    // Activation / désactivation des lumières liées à ce script
    void SwitchLumiere(EventParam evenement)
    {
        EventParamBool evenementLumiere = (EventParamBool)evenement;
        gameObject.GetComponent<Light>().enabled = evenementLumiere.Value;
    }
}
