using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionFinJeu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("startKeyboard", LancementSon);
    }

    void LancementSon(EventParam e)
    {
        // A la fin d'un serious game, on lance un son
        GetComponent<GestionSound>().PlaySon();
    }
}
