using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class temp : MonoBehaviour

{
    private Interactable interatable;
    
    // Start is called before the first frame update
    void Start()
    {
        interatable = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interatable != null && interatable.attachedToHand != null)
        {
            Debug.Log("BANANANAN");
        }
    }
}
