using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using Valve.VR;

public class rayCastFromHand : MonoBehaviour
{
    
    [SerializeField]
    private GameObject curseur;
    [SerializeField]
    private SteamVR_Action_Boolean mon_bool;
    [SerializeField]
    private SteamVR_Input_Sources mon_control;
    [SerializeField]
    public SteamVR_ActionSet playerOperationActionSet;

    void Start()
    {
        playerOperationActionSet.Activate();
        if (curseur)
            (curseur.GetComponent<Collider>()).enabled = false;
    }

    void Update()
    {

        RaycastHit hit;
        //envoie un rayon et recupere le retour
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            curseur.transform.position = hit.point;
            //on active le curseur si la gchette est appuyée
            if (mon_bool.GetStateDown(mon_control) && curseur)
            {
                (curseur.GetComponent<Collider>()).enabled = true;
            }
        }
        // si le rayon n'a pas touché on l'affiche en blanc dans le debug / scene
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        //la gachette est relache donc on desactive le curseur
        if (mon_bool.GetStateUp(mon_control) && curseur)
        {
            (curseur.GetComponent<Collider>()).enabled = false;
        }
    }
}
