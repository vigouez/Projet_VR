using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GestionDouchette : MonoBehaviour
{
    LineRenderer laser;
    RaycastHit hit;
    private Interactable interactable;
    [SerializeField]
    private bool modeTest;
    EventAffichageEcran evenementEcran;
    bool scanEnCours;

    // Start is called before the first frame update
    void Start()
    {
        laser = gameObject.GetComponent<LineRenderer>();
        laser.enabled = false;
        interactable = this.transform.parent.GetComponent<Interactable>();
        evenementEcran = new EventAffichageEcran();
        scanEnCours = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable != null && interactable.attachedToHand != null || modeTest == true)
        {
            laser.enabled = true;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //touché !
                if (hit.collider.gameObject.tag == "codeBarre")
                {
                    laser.material.color = Color.green;
                                        
                    GameObject pneu = hit.collider.gameObject.transform.parent.gameObject;
                    GestionPneu magestionPneu = pneu.GetComponent<GestionPneu>();
                    bool defectueux = magestionPneu.getDefectueux();


                    //Waiting, Error, NoScan, Conform
                    if (defectueux)
                    {
                        evenementEcran.Value = "Error";
                        GetComponent<GestionSoundMultiple>().PlaySon2();
                    }
                    else
                    {
                        evenementEcran.Value = "Conform";
                        // Lancement du bruitage
                        GetComponent<GestionSoundMultiple>().PlaySon1();
                    }
                    EventManager.TriggerEvent("AffichageEcran", evenementEcran);

                    //booleen pour gérer le cas où le laser sort du codeBarre afin de lancer la coroutine permettant de réinitialiser l'affichage de l'écran au bout de 5sec.
                    scanEnCours = true;
                }
                else {
                    if (scanEnCours == true)
                    {
                        scanEnCours = false;
                        StartCoroutine("CoroutineResteAfficherEcran");
                    }
                    laser.material.color = Color.red;
                }
                laser.SetPosition(0, gameObject.transform.position);
                laser.SetPosition(1, hit.point);
            }
            else
            {
                //aucune touche ...
                laser.material.color = Color.red;
                laser.SetPosition(0, gameObject.transform.position);
                laser.SetPosition(1, transform.TransformDirection(Vector3.forward) * 1000);
            }
        }
        else
        {
            laser.enabled = false;
        }

    }

    IEnumerator CoroutineResteAfficherEcran()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("5sec écoulées");
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
        if (hit.collider.gameObject.tag != "codeBarre")
        {
            evenementEcran.Value = "Waiting";
            EventManager.TriggerEvent("AffichageEcran", evenementEcran);
        }
    }
}
