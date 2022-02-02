using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script permettant de faire bouger un robot manipulateur orange (en contre-bas de la scène)
public class GestionAnim : MonoBehaviour
{
    public Animator anim;
    bool deja = false;
 

    public void startAnim()
    {
        if (!deja) {
            anim.Play("Anim");
            deja = true;
        }
        anim.speed = 1;
    }

    public void stopAnim()
    {
        anim.speed = 0;
    }
}
