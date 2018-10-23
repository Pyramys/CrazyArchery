using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StageManager : MonoBehaviour {
    //This handles the stage
    [SerializeField] private GameObject[] go_civilians;
    [SerializeField] private float flt_eventStartTime=45;   //Time for the event to start
    [SerializeField] private float flt_eventTimeStamp;
    [SerializeField] private float flt_eventDuration = 15;
    [SerializeField] private bool bool_isEventHappening;
    [SerializeField] private Text txt_timerText;
    [SerializeField] private Player player;
    [SerializeField] private Target target;

    public float Flt_eventStartTime
    {
        get
        {
            return flt_eventStartTime;
        }

        set
        {
            flt_eventStartTime = value;
        }
    }

    // Use this for initialization
    void Start () {
        InitializeStage();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitializeStage()
    {
        go_civilians = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    void TrackEvent()
    {
        if(bool_isEventHappening)
        {
            if(Time.time - flt_eventTimeStamp >= flt_eventDuration)
            {
                EndEvent();
            }
        }
    }

    public void StartEvent()
    {
        txt_timerText.color = Color.red; 
        bool_isEventHappening = true;
        flt_eventTimeStamp = Time.time;
        foreach(GameObject go in go_civilians)
        {
            go.GetComponent<Obstacle>().Enrage();
        }
        InvokeRepeating("TrackEvent", 1,1);
    }

    public void EndEvent()
    {
        txt_timerText.color = Color.black;
        foreach (GameObject go in go_civilians)
        {
            go.GetComponent<Obstacle>().Unrage();
        }
        CancelInvoke("TrackEvent");
        bool_isEventHappening = false;
        Debug.Log("Ended Event");
    }

    public void EndStage()
    {
        target.End();
        //stop the scene
        foreach (GameObject go in go_civilians)
        {
            go.GetComponent<Obstacle>().End();
        }
        //Zoom into the target to view, linger until player taps screen
        player.MoveToTarget();
        //disable the scene
        //Trigger Ad
        //Bring up score Screen
        // Change stars color based on performance
    }
}
