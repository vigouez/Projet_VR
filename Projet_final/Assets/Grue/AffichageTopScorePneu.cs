using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageTopScorePneu : MonoBehaviour
{
    Text txt;
    string _scores;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        EventManager.StartListening("changementScorePneu", chgScores);
    }

    // Update is called once per frame
    void Update()
    {
        affiche();
    }

    void chgScores(EventParam e) {
        _scores = "        Top Scores :\n\n";
        _scores += ((EventParamHighScores)e).getStrings();
    }

    void affiche() {
        txt.text = _scores;
    }
}
