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
        GetComponent<GestionSound>().PlaySon();
    }

    void ArretSon(EventParam e)
    {
        GetComponent<GestionSound>().StopSon();
    }
}
