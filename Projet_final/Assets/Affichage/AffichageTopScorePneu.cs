using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageTopScorePneu : MonoBehaviour
{
    Text txt;
    string _scores;

    void Start()
    {
        txt = GetComponent<Text>();
        EventManager.StartListening("changementScorePneu", chgScores);
    }

    void Update()
    {
        affiche();
    }

    // A chaque ajout d'un nouveau score, re-affichage des scores
    void chgScores(EventParam e) {
        _scores = "        Top Scores :\n\n";
        _scores += ((EventParamHighScores)e).getStrings();
    }

    void affiche() {
        txt.text = _scores;
    }
}
