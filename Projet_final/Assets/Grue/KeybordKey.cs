using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordKey : MonoBehaviour
{
    [SerializeField]
    private char c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void keyPressed() {
        // Event d'envoie de la touche pressée sur le clavier virtuel
        EventManager.TriggerEvent("key", new EventParamKey(c));
    }
}
