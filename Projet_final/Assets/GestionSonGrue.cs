using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSonGrue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("startChemin", LancementSon);
        EventManager.StartListening("stopChemin", ArretSon);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LancementSon(EventParam e)
    {
        // Le son est joué pendant toute la durée du serious game de la grue
        GetComponent<GestionSound>().PlaySon();
    }

    void ArretSon(EventParam e)
    {
        // On stop le son de la grue à la fin du serious game
        GetComponent<GestionSound>().StopSon();
    }
}
