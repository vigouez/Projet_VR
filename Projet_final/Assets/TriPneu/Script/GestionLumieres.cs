using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionLumieres : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(gameObject.name, SwitchLumiere);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchLumiere(EventParam evenement)
    {
        EventParamBool evenementLumiere = (EventParamBool)evenement;
        gameObject.GetComponent<Light>().enabled = evenementLumiere.Value;
    }
}
