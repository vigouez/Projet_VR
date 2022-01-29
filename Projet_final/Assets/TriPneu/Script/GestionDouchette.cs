using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GestionDouchette : MonoBehaviour
{
    LineRenderer laser;
    RaycastHit hit;
    private Interactable interactable;
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
        // Si la douchette est prise à la main
        if (interactable != null && interactable.attachedToHand != null)
        {
            // Activation du laser
            laser.enabled = true;

            // Si le raycast touche un objet
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                // Si le raycast touche l'objet du code-barres
                if (hit.collider.gameObject.tag == "codeBarre")
                {
                    laser.material.color = Color.green;
                                        
                    GameObject pneu = hit.collider.gameObject.transform.parent.gameObject;
                    // On regarde si le pneu est défectueux
                    GestionPneu magestionPneu = pneu.GetComponent<GestionPneu>();
                    bool defectueux = magestionPneu.getDefectueux();


                    // Affichage écran : Error, Conform
                    if (defectueux)
                    {
                        // On affiche l'image d'erreur sur l'écran de la console
                        evenementEcran.Value = "Error";
                        // Lancement du bruitage
                        GetComponent<GestionSoundMultiple>().PlaySon2();
                    }
                    else
                    {
                        evenementEcran.Value = "Conform";
                        GetComponent<GestionSoundMultiple>().PlaySon1();
                    }
                    // Lancement de l'event permettant d'afficher la bonne image sur l'écran de la console
                    EventManager.TriggerEvent("AffichageEcran", evenementEcran);

                    // Booleen pour gérer le cas où le laser sort du codeBarre, afin de lancer la coroutine permettant de réinitialiser l'affichage de l'écran au bout de 5sec.
                    scanEnCours = true;
                }
                else {  // Si le raycast touche un autre objet que le code-barres
                    // Si on vient de toucher le code-barres
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
            // Si le raycast ne touche aucun objet
            else
            {
                laser.material.color = Color.red;
                laser.SetPosition(0, gameObject.transform.position);
                laser.SetPosition(1, transform.TransformDirection(Vector3.forward) * 1000);
            }
        }
        // Si la douchette n'est pas prise à la main
        else
        {
            // Désactivation du laser
            laser.enabled = false;
        }

    }

    // Coroutine permettant de laisser affiché l'image de l'écran de la console pendant 5 secondes
    IEnumerator CoroutineResteAfficherEcran()
    {
        yield return new WaitForSeconds(5.0f);

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
        // Si au bout des 5 secondes, on ne touche plus le code-barres, alors on affiche 'Waiting' sur l'écran
        if (hit.collider.gameObject.tag != "codeBarre")
        {
            evenementEcran.Value = "Waiting";
            EventManager.TriggerEvent("AffichageEcran", evenementEcran);
        }
    }
}
