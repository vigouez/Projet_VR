using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordKey : MonoBehaviour
{
    [SerializeField]
    private char c;

    void OnCollisionEnter(Collision collision) {
        keyPressed();
    }

    void keyPressed() {
        // Event d'envoie de la touche pressée sur le clavier virtuel
        EventManager.TriggerEvent("key", new EventParamKey(c));
    }
}
