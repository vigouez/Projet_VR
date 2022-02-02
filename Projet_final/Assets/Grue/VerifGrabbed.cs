using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VerifGrabbed : MonoBehaviour
{
    private Interactable interactable;
    private bool isGrabbed;
    void Start()
    {
        interactable = GetComponent<Interactable>();
        isGrabbed = false;
    }

    
    void Update()
    {
        //il faut que la tête de la grue soit attrapée afin que le jeu puisse commencer, afin d'éviter que le jeu démarre lorsqsu'on la lache
        if (interactable && interactable.attachedToHand && !isGrabbed)
        {
            grabbed();
        }
        else if(interactable && !interactable.attachedToHand && isGrabbed)
        {
            ungrabbed();
        }
    }

    // envoie un event quand on l'attrape 
    void grabbed(){
        isGrabbed = true;
        EventManager.TriggerEvent("Grabbed", null);
    }

    // envoie un event quand on le lache 
    void ungrabbed() {
        isGrabbed = false;
        EventManager.TriggerEvent("Ungrabbed", null);
    }
}
