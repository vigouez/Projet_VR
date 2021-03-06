using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
//using EventManager;

public class GestionScore : MonoBehaviour
{
    // Fichiers de sauvegarde des scores pour le jeu du tri des pneus
    [SerializeField]
    string filenamePneu = "Assets/Score/scorePneu.txt";

    // Fichiers de sauvegarde des scores pour le jeu de la grue
    [SerializeField]
    string filenameGrue = "Assets/Score/scoreGrue.txt";
    
    DataTable infoGrue;
    DataTable infoPneu;
    DataView viewGrue;
    DataView viewPneu;
    
    // Start is called before the first frame update
    void Start()
    {
        infoGrue = new DataTable("grue");
        infoPneu = new DataTable("pneu");
        infoGrue.Columns.Add("Nom");
        infoGrue.Columns.Add("score", Type.GetType("System.Decimal"));
        infoGrue.Columns.Add("time", Type.GetType("System.Decimal"));
        infoPneu.Columns.Add("Nom");
        infoPneu.Columns.Add("time",Type.GetType("System.Decimal"));
        viewGrue = new DataView(infoGrue);
        
        viewPneu = new DataView(infoPneu);
        
        getScore();
        dumpScore();
        EventManager.StartListening("sendScore", addScoreEvent);
    }

    //recupere les scores
    void getScore()
    {
        //recupere les scores du serious games des pneus
        string[] lines = System.IO.File.ReadAllLines(filenamePneu);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] linecut = lines[i].Split(':');
            string name = linecut[0].TrimEnd(' ');
            float time;
            if (!float.TryParse(linecut[1], out time)) time = 0;

            infoPneu.Rows.Add(name, time);
        }
        infoPneu.AcceptChanges();
        //recupere les scores du serious games des grue
        lines = System.IO.File.ReadAllLines(filenameGrue);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] linecut = lines[i].Split(':');
            string name = linecut[0].TrimEnd(' ');
            int scoreLine;
            if (!int.TryParse(linecut[1], out scoreLine)) scoreLine = 0;
            float time;
            if (!float.TryParse(linecut[2], out time)) time = 0;

            infoGrue.Rows.Add(name, scoreLine, time);
        }
        infoGrue.AcceptChanges();

        viewPneu.Sort = "time ASC";
        viewGrue.Sort = "score DESC, time ASC";
    }

    //ecrit les scores dans un fichier 
    void dumpScore()
    {
        // Gestion scores pour le serious game des pneus
        string[] stringScores = new string[viewPneu.Count];
        int i = 0;
        foreach (DataRowView row in viewPneu)
        {
            stringScores[i++] = string.Format("{0} : {1}", row["Nom"], row["time"]);
        }
        System.IO.File.WriteAllLines(filenamePneu, stringScores);
        EventManager.TriggerEvent("changementScorePneu",new EventParamHighScores(stringScores));
        Debug.Log("here");

        // Gestion scores pour le serious game de la grue
        stringScores = new string[viewGrue.Count];
        i = 0;
        foreach (DataRowView row in viewGrue)
        {
            stringScores[i++] = string.Format("{0} : {1} : {2}", row["Nom"], row["score"], row["time"]);
        }
        System.IO.File.WriteAllLines(filenameGrue, stringScores);
        EventManager.TriggerEvent("changementScoreGrue", new EventParamHighScores(stringScores));
    }

    void addScoreEvent(EventParam e) {
        addScore(((EventParamScore)e).getType(), ((EventParamScore)e).getName(), ((EventParamScore)e).getScore(), ((EventParamScore)e).getTime());
    }

    //ajoute le score au fichier correspondant au type de jeu qui a été réalisé
    void addScore(string type, string name, int score,float time) {
        switch (type) {
            case "pneu":
                infoPneu.Rows.Add(name, time);
                infoPneu.AcceptChanges();
                // Tri des performances par les temps croissants
                viewPneu.Sort = "time ASC";
                dumpScore();
                break;
            case "grue":
                infoGrue.Rows.Add(name,score,time);
                infoGrue.AcceptChanges();
                // Tri des performances par les scores décroissants, puis par les temps croissants
                viewGrue.Sort = "score DESC, time ASC";
                dumpScore();
                break;
            default:
                break;
        }
    }
}
