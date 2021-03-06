using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Diagnostics;

public class AffichageScore_Chrono : MonoBehaviour
{
    float score;
    float time;
    float dist;
    bool hasStarted;
    string type;
    bool estDansLErreur;
    Stopwatch stopWatch = new Stopwatch();
    Text txt;
    
    void Start()
    {
        type = "";
        txt = GetComponent<Text>();
        txt.text = "Standby";
        score = 0;
        dist = 0;
        time = 0;
        hasStarted = false;
        estDansLErreur = false;

        EventManager.StartListening("startChemin", startChemin);
        EventManager.StartListening("stopChemin", stopChemin);
        EventManager.StartListening("infoDist", infoDist);
        EventManager.StartListening("startPneus", startPneus);
        EventManager.StartListening("stopPneus", stopPneus);
        EventManager.StartListening("ResetNombrePneus", boutonRougePneu);
    }
     

    void Update()
    {
        affiche();
    }

    // Affiche le temps écoulé en fonction du serious game lancé
    void affiche() {
        System.TimeSpan ts = stopWatch.Elapsed;

        string elapsedTime = string.Format("{1:00}min{2:00}s{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        time = ts.Minutes*60 + ts.Seconds + ts.Milliseconds/1000f;

        if (type == "grue"){
            if (hasStarted )
            {
                txt.text = "Score :\n" + (int)score + "\nChrono :\n" + elapsedTime + "s\nEcart à la perfection :\n" + dist + "m";
            }
            if (hasStarted && estDansLErreur) {
                score -= Time.deltaTime*2;
            }
        }
        else if (type == "pneu")
        {
            if (hasStarted)
            {
                txt.text = "Chrono :\n" + elapsedTime;
            }
        }
    }

    //demarre le chrono
    void startChemin(EventParam e)
    {
        type = "grue";
        hasStarted = true;
        time = 0;
        score = 100;
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    //arrete le chrono
    void stopChemin(EventParam e)
    {
        hasStarted = false;
        stopWatch.Stop();
        EventManager.TriggerEvent("startKeyboard", new EventParamScore("grue", "",(int)score,time));
    }

    //demarre le chrono
    void startPneus(EventParam e)
    {
        type = "pneu";
        hasStarted = true;
        time = 0;
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    //arrete le chrono
    void stopPneus(EventParam e)
    {
        hasStarted = false;
        stopWatch.Stop();
        EventManager.TriggerEvent("startKeyboard", new EventParamScore("pneu", "",(int)time, time));
    }

    //arrête le jeu afin qu'on ne puisse pas sauvegarder notre score lorsque l'utilisateur appui sur le bouton rouge
    void boutonRougePneu(EventParam e)
    {
        hasStarted = false;
        stopWatch.Stop();
    }

    // Permet de gérer si l'opérateur s'éloigne ou non de la ligne à suivre (serious game de la grue)
    void infoDist(EventParam e)
    {
        dist = ((EventParamFloat)e).getFloat();
        if (dist > 0.3)
        {
            estDansLErreur = true;
        }
        else {
            estDansLErreur = false;
        }
    }

}
