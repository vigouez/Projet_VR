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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "multiple_collider_fix")
        {
            GameObject pneu = other.gameObject.transform.parent.gameObject;
            GestionPneu magestionPneu = pneu.GetComponent<GestionPneu>();
            bool lisible = magestionPneu.getLisible();
            bool defectueux = magestionPneu.getDefectueux();

            //Waiting, Error, NoScan, Conform
            if (lisible)
            {
                if (defectueux)
                {
                    evenementEcran.Value = "Error";
                    GetComponent<GestionSoundMultiple>().PlaySon2();
                }
                else
                {
                    evenementEcran.Value = "Conform";
                    GetComponent<GestionSoundMultiple>().PlaySon1();
                }
            }
            else
            {
                evenementEcran.Value = "NoScan";
            }

            EventManager.TriggerEvent("AffichageEcran", evenementEcran);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        evenementEcran.Value = "Waiting";
        EventManager.TriggerEvent("AffichageEcran", evenementEcran);
    }
}
