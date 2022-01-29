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

    // Quand on rentre dans une des 2 bennes (pneu OK ; pneu KO)
    private void OnTriggerEnter(Collider other)
    {
        // Si l'objet entré est un pneu
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GestionPneu magestionPneu = other.gameObject.transform.parent.gameObject.GetComponent<GestionPneu>();
            bool defectueux = magestionPneu.getDefectueux();

            // Si la benne collisionnée par le pneu est la verte
            if (gameObject.name == "DetectionContainerGreen")
            {
                // Si le pneu n'est pas défectueux
                if(!defectueux)
                {
                    // On ajoute 1 au nombre de pneus correctement catégorisés
                    evenementComptage.Value = 1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
            // Si la benne collisionnée par le pneu est la rouge
            else if (gameObject.name == "DetectionContainerRed")
            {
                // Si le pneu est défectueux
                if (defectueux)
                {
                    // On ajoute 1 au nombre de pneus correctement catégorisés
                    evenementComptage.Value = 1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
        }
    }

    // Quand on sort d'une des 2 bennes (pneu OK ; pneu KO)
    private void OnTriggerExit(Collider other)
    {
        // Si l'objet sorti est un pneu
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GestionPneu magestionPneu = other.gameObject.transform.parent.gameObject.GetComponent<GestionPneu>();
            bool defectueux = magestionPneu.getDefectueux();

            // Si la benne collisionnée par le pneu est la verte
            if (gameObject.name == "DetectionContainerGreen")
            {
                // Si le pneu n'est pas défectueux
                if (!defectueux)
                {
                    // On enlève 1 au nombre de pneus correctement catégorisés
                    evenementComptage.Value = -1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
            // Si la benne collisionnée par le pneu est la rouge
            else if (gameObject.name == "DetectionContainerRed")
            {
                // Si le pneu est défectueux
                if (defectueux)
                {
                    // On enlève 1 au nombre de pneus correctement catégorisés
                    evenementComptage.Value = -1;
                    EventManager.TriggerEvent("Comptage", evenementComptage);
                }
            }
        }
    }
}
