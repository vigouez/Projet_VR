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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))//envoie un rayon et recupere le retour
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            curseur.transform.position = hit.point;
            if (mon_bool.GetStateDown(mon_control) && curseur)//la gachette est appuye
            {
                (curseur.GetComponent<Collider>()).enabled = true;
            }
        }
        else Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); // si le rayon n'a pas touché on l'affiche en blanc dans le debug / scene
        
        if (mon_bool.GetStateUp(mon_control) && curseur)//la gachette est relache
        {
            (curseur.GetComponent<Collider>()).enabled = false;
        }
    }
}
