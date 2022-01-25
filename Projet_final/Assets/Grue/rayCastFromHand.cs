using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using Valve.VR;
//using System.Runtime.InteropServices;
//using System.Collections.Generic;

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
    }

    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.gameObject != null)
            {
                if (mon_bool.GetStateDown(mon_control))
                {
                    //Debug.Log("il y a un appui 1 et une touche");
                    //Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage("keyPressed");
                }
                curseur.transform.position = hit.point;
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    void FixedUpdate()
    {

        
    }
}
