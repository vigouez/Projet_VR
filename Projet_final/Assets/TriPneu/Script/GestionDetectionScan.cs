using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDetectionScan : MonoBehaviour
{
    EventAffichageEcran evenementEcran;

    // Start is called before the first frame update
    void Start()
    {
        evenementEcran = new EventAffichageEcran();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Quand on rentre dans la zone de détection de la table
    private void OnTriggerEnter(Collider other)
    {
        // Si l'objet entré est un pneu
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GameObject pneu = other.gameObject.transform.parent.gameObject;
            GestionPneu magestionPneu = pneu.GetComponent<GestionPneu>();
            bool lisible = magestionPneu.getLisible();
            bool defectueux = magestionPneu.getDefectueux();

            // Si le pneu est lisible (si la détection peut se faire automatiquement)
            if (lisible)
            {
                // Si le pneu est défectueux
                if (defectueux)
                {
                    // Affichage de l'écran 'Error' sur la console
                    evenementEcran.Value = "Error";
                    GetComponent<GestionSoundMultiple>().PlaySon2();
                }
                // Si le pneu n'est pas défectueux
                else
                {
                    evenementEcran.Value = "Conform";
                    GetComponent<GestionSoundMultiple>().PlaySon1();
                }
            }
            // Si le pneu n'est pas lisible
            else
            {
                evenementEcran.Value = "NoScan";
            }

            EventManager.TriggerEvent("AffichageEcran", evenementEcran);
        }
    }

    // Quand on sort de la zone de détection de la table
    private void OnTriggerExit(Collider other)
    {
        evenementEcran.Value = "Waiting";
        EventManager.TriggerEvent("AffichageEcran", evenementEcran);
    }
}
