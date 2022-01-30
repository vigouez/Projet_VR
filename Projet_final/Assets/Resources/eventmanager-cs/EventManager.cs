using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<EventParam>> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent += listener;

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        if (eventManager == null) return;
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
            // OR USE  instance.eventDictionary[eventName](eventParam);
        }
    }
}

//Re-usable structure/ Can be a class to. Add all parameters you need inside it
public class EventParam
{

}
public class EventParamGameObject : EventParam
{
    GameObject _gameobject;
    public EventParamGameObject(GameObject gameobject) {
        _gameobject = gameobject;
    }
    public GameObject getGameObject()
    {
        return _gameobject;
    }
    public void setGameObject(GameObject gameobject)
    {
        _gameobject = gameobject;
    }
}

public class EventParamFloat : EventParam
{
    float _f;

    public EventParamFloat(float f)
    {
        _f = f;
    }
    public float getFloat()
    {
        return _f;
    }
    public void setFloat(float f)
    {
        _f = f;
    }
}

public class EventParamKey : EventParam
{
    char _c;

    public EventParamKey(char c)
    {
        _c = c;
    }
    public char getChar()
    {
        return _c;
    }
    public void setChar(char c)
    {
        _c = c;
    }
}

public class EventParamScore : EventParam
{
    string _type, _name;
    int _score;
    float _time;

    public EventParamScore(string type, string name, int score,float time)
    {
        _type = type;
        _name = name;
        _score = score;
        _time = time;
    }

    public string getType()
    {
        return _type;
    }
    public string getName()
    {
        return _name;
    }
    public int getScore()
    {
        return _score;
    }
    public float getTime()
    {
        return _time;
    }

}

public class EventParamHighScores : EventParam {
    string _scores;
    public EventParamHighScores(string[] stringScores)
    {
        _scores = "";
        for (int i = 0; i < Mathf.Min(10, stringScores.Length); i++) {
            _scores += (i+1) +" - "+ stringScores[i] + "sec\n";
        }
    }

    public string getStrings()
    {
        return _scores;
    }
}

public class EventParamBool : EventParam
{
    private bool value;
    public bool Value
    {
        get => value; set => this.value = value; }
    }

    public class EventAffichageEcran : EventParam
    {
        private String value;
        public String Value
        {
            get => value; set => this.value = value; }
        }

        public class EventComptagePneus : EventParam
        {
            private int value;
            public int Value
            {
                get => value; set => this.value = value; }
            }
