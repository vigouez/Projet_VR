using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool cheminIsShown;  
    private GameObject gameObjectEffecteur;

    // Start is called before the first frame update
    void Start()
    {
        cheminIsShown = false;
        EventManager.StartListening("startChemin", startChemin);
        EventManager.StartListening("stopChemin", stopChemin);

        if(gameObject.tag == "Chemin"){
            GetComponent<Renderer>().enabled = cheminIsShown;
            GetComponent<LineRenderer>().enabled = cheminIsShown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Chemin"){
            computeCheminToPoint(gameObjectEffecteur);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Effecteur" && gameObject.tag == "Depart" && !cheminIsShown) {
            EventParamGameObject e = new EventParamGameObject();
            e.setGameObject(other.gameObject);
            EventManager.TriggerEvent("startChemin",e);
        }
        if (other.gameObject == gameObjectEffecteur && gameObject.tag == "Chemin" && cheminIsShown) {
            EventManager.TriggerEvent("stopChemin", null);
        }
    }

    void startChemin(EventParam e)
    {
        cheminIsShown = true;

        if (gameObject.tag == "Chemin")
        {
            initPoint();
            gameObjectEffecteur = ((EventParamGameObject)e).getGameObject();
            GetComponent<Renderer>().enabled = cheminIsShown;
            GetComponent<LineRenderer>().enabled = cheminIsShown;
        }
    }
    void stopChemin(EventParam e)
    {
        cheminIsShown = false;

        if(gameObject.tag == "Chemin"){
            GetComponent<Renderer>().enabled = cheminIsShown;
            GetComponent<LineRenderer>().enabled = cheminIsShown;
        }
    }


    void computeCheminToPoint(GameObject gameObjectEffecteur)
    {
        if (cheminIsShown && gameObjectEffecteur != null && gameObject.tag == "Chemin"){
            Vector3 positionObject = gameObjectEffecteur.GetComponent<Transform>().position;
            LineRenderer lr = GetComponent<LineRenderer>();
            Vector3[] positionLinePoints = new Vector3[lr.positionCount];

            lr.GetPositions(positionLinePoints);

            float distMin = 100.0f;

            for(int i = 0;i<lr.positionCount-1;i++){
                Vector3 line = positionLinePoints[i+1] - positionLinePoints[i];
                Vector3 point = positionObject - positionLinePoints[i];
                Vector3 distPointOnLine = Vector3.Project(point , line);

                float ratio = distPointOnLine.magnitude/line.magnitude;
                float dist;

                if(0<ratio && ratio<1){ // Point projete sur la ligne
                    dist = (point - distPointOnLine).magnitude;
                }else{  // Point en dehors de la ligne
                    dist = point.magnitude;
                }
                if(dist<distMin){
                    distMin = dist;
                }
            }

            Vector3 lastpointtoline = positionObject - positionLinePoints[lr.positionCount-1];

            if (lastpointtoline.magnitude<distMin){
                distMin = lastpointtoline.magnitude;
            }
            
            EventManager.TriggerEvent("infoDist", new EventParamFloat(distMin));

            if (distMin > 0.3)
            {
                Color c = Color.red;
                lr.startColor = c;
                lr.endColor = c;
            }
            else {
                Color c = Color.green;
                lr.startColor = c;
                lr.endColor = c;
            }

        }
    }

    void initPoint(){
        LineRenderer lr = GetComponent<LineRenderer>();
        Vector3 start = lr.GetPosition(0);
        Vector3 stop = lr.GetPosition(lr.positionCount-1);

        float t = Random.value*4;
        int size = 4 + (int)Mathf.Ceil(t);

        Vector3[] positions = new Vector3[size];
        
        for(int i=1;i<size-1;i++){
            float x = (Random.value-0.5f)*0.3f;
            float y = Mathf.Sin(Mathf.PI*i/(size-1))*0.5f+(Random.value-0.5f)*0.3f;
            float z = Mathf.Sin(Mathf.PI*i/(size-1));

            Vector3 rand = new Vector3(x,y,z);

            positions[i] = (start*(size-1-i)+stop*i)/(size-1)+rand;
        }

        positions[0] = start;
        positions[size-1] = stop;
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }
}
