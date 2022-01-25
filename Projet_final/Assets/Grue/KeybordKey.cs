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
        Debug.Log("key");
        EventManager.TriggerEvent("key", new EventParamKey(c));
    }
}
