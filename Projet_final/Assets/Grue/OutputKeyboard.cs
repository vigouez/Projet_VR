using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputKeyboard : MonoBehaviour
{
    Text txt;
    string name;
    string type;
    int score;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        name = "";
        score = 0;
        txt.text = name + score;
        EventManager.StartListening("key", keyPressed);
        EventManager.StartListening("startKeyboard", startKeyboard);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void keyPressed(EventParam e)
    {
        char c = ((EventParamKey)e).getChar();
        Debug.Log("la touche : " + c);
        switch (c) {
            case '+':
                Debug.Log("event sendScore" + name);
                if(name!="")
                    EventManager.TriggerEvent("sendScore", new EventParamScore(type, name, score,time));
                type = "last";
                name = "";
                score = 0;
                time = 0;
                break;
            case '-':
                name = name.Remove(txt.text.Length - 1, 1);
                break;
            default:
                if (name.Length < 18)
                {
                    name = name + "" + c;
                }
                break;
        }
        txt.text = name + " : " + score;

    }

    void startKeyboard(EventParam e)
    {
        type = ((EventParamScore)e).getType();
        name = ((EventParamScore)e).getName();
        score = ((EventParamScore)e).getScore();
        time = ((EventParamScore)e).getTime();
        txt.text = name + score;
    }

}
