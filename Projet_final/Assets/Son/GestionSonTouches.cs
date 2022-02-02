using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSonTouches : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Ecoute de l'event pour savoir si une touche du clavier virtuel est touchée
        EventManager.StartListening("key", LancementSon);
    }

    // A chaque fois qu'une touche du clavier virtuel est touchée, on lance le son associé
    void LancementSon(EventParam e)
    {
        GetComponent<GestionSound>().PlaySon();
    }
}
