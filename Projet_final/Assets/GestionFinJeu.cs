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

    // Update is called once per frame
    void Update()
    {
        
    }

    void LancementSon(EventParam e)
    {
        GetComponent<GestionSound>().PlaySon();
    }
}
