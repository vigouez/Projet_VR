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

    void Start()
    {
        txt = GetComponent<Text>();
        name = "";
        score = 0;
        txt.text = name + score;
        // Récupération de la touche pressée sur le clavier virtuel
        EventManager.StartListening("key", keyPressed);
        EventManager.StartListening("startKeyboard", startKeyboard);
    }

    void keyPressed(EventParam e)
    {
        // Caractère de la touche
        char c = ((EventParamKey)e).getChar();
        switch (c) {
            // La touche "Entrée" correspond au caractère '+'
            case '+':
                if (name != "")
                {
                    EventManager.TriggerEvent("sendScore", new EventParamScore(type, name, score, time));
                    type = "last";
                    name = "";
                    score = 0;
                    time = 0;
                }
                break;
            // Si c'est la touche "Supprimer"
            case '-':
                name = name.Remove(name.Length - 1, 1);
                break;
            default:
                // Le nom de l'opérateur ne doit pas dépasser les 18 caractères
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
