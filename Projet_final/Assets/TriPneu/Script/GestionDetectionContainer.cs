using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDetectionContainer : MonoBehaviour
{

    EventComptagePneus evenementComptage;

    // Start is called before the first frame update
    void Start()
    {
        evenementComptage = new EventComptagePneus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GestionPneu magestionPneu = other.gameObject.transform.parent.gameObject.GetComponent<GestionPneu>();
            bool defectueux = magestionPneu.getDefectueux();

            if (gameObject.name == "DetectionContainerGreen")
            {
                if(!defectueux)
                {
                    evenementComptage.Value = 1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
            else if(gameObject.name == "DetectionContainerRed")
            {
                if (defectueux)
                {
                    evenementComptage.Value = 1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GestionPneu magestionPneu = other.gameObject.transform.parent.gameObject.GetComponent<GestionPneu>();
            bool defectueux = magestionPneu.getDefectueux();

            if (gameObject.name == "DetectionContainerGreen")
            {
                if (!defectueux)
                {
                    evenementComptage.Value = -1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
            else if (gameObject.name == "DetectionContainerRed")
            {
                if (defectueux)
                {
                    evenementComptage.Value = -1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
        }
    }
}
